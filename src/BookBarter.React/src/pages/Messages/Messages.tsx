import { useState, useRef, useCallback, useEffect, useMemo } from 'react'
import { useNavigate } from 'react-router-dom'
import {
  List,
  ListItemButton,
  ListItemText,
  TextField,
  IconButton,
  CircularProgress,
  Divider,
  Button,
  debounce
} from '@mui/material'
import SendIcon from '@mui/icons-material/Send'
import * as signalR from "@microsoft/signalr"
import { useAuth } from '../../contexts/Auth/UseAuth'
import { useNotification } from '../../contexts/Notification/UseNotification'
import styles from './Messages.module.css'
import HomeButton from '../../components/Navigation/HomeButton'
import type { MessagingUserDto } from '../../api/generated/models/messaging-user-dto'
import type { MessageDto } from '../../api/generated/models/message-dto'
import type { CreateMessageCommand } from '../../api/generated'
import { fetchUserChats } from '../../api/clients/user-client'
import { fetchMessagesPaginatedResult } from '../../api/clients/message-client'
import LoadingSpinner from '../../components/LoadingSpinner/LoadingSpinner'

const MESSAGES_PAGE_SIZE = 20
const SCROLL_THRESHOLD_PERCENTAGE = 0.4
const MESSAGE_RESOLVE_TIMEOUT = 10000

interface OptimisticMessage extends MessageDto {
  tempId?: string
}

const Messages = () => {
  const { userAuthData } = useAuth()
  const { showNotification } = useNotification()
  const navigate = useNavigate()

  const [chats, setChats] = useState<MessagingUserDto[]>([])
  const [selectedChat, setSelectedChat] = useState<MessagingUserDto | null>(null)
  const [messages, setMessages] = useState<OptimisticMessage[]>([])
  const [loadingChats, setLoadingChats] = useState(true)
  const [loadingMessages, setLoadingMessages] = useState(false)
  const [loadingMore, setLoadingMore] = useState(false)
  const [hasMore, setHasMore] = useState(true)
  const [page, setPage] = useState(1)
  const [messageText, setMessageText] = useState('')
  const [isConnected, setIsConnected] = useState(false)

  const messagesContainerRef = useRef<HTMLDivElement>(null)

  const connectionRef = useRef<signalR.HubConnection | null>(null)
  const selectedChatIdRef = useRef<number | null>(null)
  const pendingMessagesRef = useRef<Map<string, any>>(new Map())

  useEffect(() => {
    selectedChatIdRef.current = selectedChat?.id || null
  }, [selectedChat])

  // SignalR connection setup
  useEffect(() => {
    if (!userAuthData?.id) return

    const connection = new signalR.HubConnectionBuilder()
      .withUrl(`${import.meta.env.VITE_API_BASE_URL}/messageHub`, {
        accessTokenFactory: () => localStorage.getItem('authToken') || ''
      })
      .configureLogging(signalR.LogLevel.Debug)
      .withAutomaticReconnect()
      .build()

    connectionRef.current = connection

    connection.on("ReceiveMessage", (message: MessageDto) => {
      const currentSelectedChatId = selectedChatIdRef.current
      const currentUserId = userAuthData.id

      // Check if this message belongs to the current conversation
      const isCurrentConversation = currentSelectedChatId && (
        (message.senderId == currentSelectedChatId && message.receiverId == currentUserId) ||
        (message.senderId == currentUserId && message.receiverId == currentSelectedChatId)
      )

      if (isCurrentConversation) {
        setMessages(prev => {
          // If this is from us, we need to replace the optimistic message
          if (message.senderId == currentUserId) {
            const updatedMessages = prev.map(prevMessage => {
              if (prevMessage.tempId &&
                prevMessage.receiverId == message.receiverId &&
                prevMessage.body === message.body &&
                prevMessage.senderId == currentUserId) {

                const timeout = pendingMessagesRef.current.get(prevMessage.tempId)
                if (timeout) {
                  clearTimeout(timeout)
                  pendingMessagesRef.current.delete(prevMessage.tempId)
                }

                return message
              }
              return prevMessage
            })
            return updatedMessages
          } else {
            // This gets called twice when in StrictMode, causing the message to appear two times
            return [...prev, message]
          }
        })
      }

      setChats(prevChats =>
        prevChats.map(chat => {
          if (chat.id == message.senderId && message.receiverId == currentUserId ||
            chat.id == message.receiverId && message.senderId == currentUserId) {
            return { ...chat, lastMessage: message.body }
          }
          return chat
        })
      )
    })

    const startConnection = async () => {
      try {
        await connection.start()
        setIsConnected(true)
      } catch (error) {
        console.error("SignalR connection error:", error)

        // Strict mode causes the first connection attempt to fail
        // Retry silently first
        setTimeout(() => {
          if (connection.state === signalR.HubConnectionState.Disconnected) {
            startConnection()
          }
        }, 3000)
      }
    }

    connection.onclose(() => {
      setIsConnected(false)
    })

    connection.onreconnected(() => {
      setIsConnected(true)
    })

    startConnection()

    return () => {
      pendingMessagesRef.current.forEach(timeout => clearTimeout(timeout))
      pendingMessagesRef.current.clear()
      connection.stop()
    }
  }, [userAuthData?.id, showNotification])

  useEffect(() => {
    loadChats()
  }, [])

  const loadChats = useCallback(async () => {
    try {
      setLoadingChats(true)
      const data = await fetchUserChats()
      setChats(data)
    } catch (error) {
      showNotification('Failed to load chats.', 'error')
    } finally {
      setLoadingChats(false)
    }
  }, [showNotification])

  const loadMessages = useCallback(async (userId: number, pageNum: number = 1) => {
    try {
      if (pageNum === 1) {
        setLoadingMessages(true)
      } else {
        setLoadingMore(true)
      }

      const response = await fetchMessagesPaginatedResult({
        pageNumber: pageNum,
        pageSize: MESSAGES_PAGE_SIZE,
        orderByProperty: "senttime",
        orderDirection: "desc",
        collocutorId: userId
      })

      const reversedItems = response.items?.reverse() || []

      if (pageNum === 1) {
        setMessages(reversedItems)
        setPage(1)
      } else {
        setMessages(prev => [...reversedItems, ...prev])
      }

      setHasMore((response.total || 0) > pageNum * MESSAGES_PAGE_SIZE)
    } catch (error) {
      showNotification('Failed to load messages.', 'error')
    } finally {
      setLoadingMessages(false)
      setLoadingMore(false)
    }
  }, [showNotification])

  const handleChatSelect = useCallback((chat: MessagingUserDto) => {
    if (chat.id == selectedChat?.id) return

    setSelectedChat(chat)
    setMessages([])
    setPage(1)
    setHasMore(true)
    loadMessages(chat.id!, 1)
  }, [selectedChat?.id, loadMessages])

  const handleScroll = useCallback(() => {
    if (!messagesContainerRef.current || loadingMore || !hasMore || !selectedChat) return

    const { scrollHeight, scrollTop } = messagesContainerRef.current

    // scrollTop is negative
    if ((scrollHeight + scrollTop) < scrollHeight * SCROLL_THRESHOLD_PERCENTAGE) {
      const nextPage = page + 1
      setPage(nextPage)
      loadMessages(selectedChat.id!, nextPage)
    }
  }, [loadingMore, hasMore, selectedChat, page, loadMessages])

  const scrollToBottom = useCallback(() => {
    if (messagesContainerRef.current) {
      messagesContainerRef.current.scrollTop = 0
    }
  }, [])

  const handleSendMessage = useCallback(async () => {
    if (!messageText.trim() || !selectedChat || !connectionRef.current || !isConnected) return

    const messageBody = messageText.trim()
    const tempId = `temp-${Date.now()}-${Math.random()}`

    try {
      const optimisticMessage: OptimisticMessage = {
        tempId,
        senderId: userAuthData?.id,
        receiverId: selectedChat.id,
        body: messageBody,
        sentTime: new Date().toISOString()
      }

      setMessages(prev => [...prev, optimisticMessage])
      setMessageText('')

      scrollToBottom()

      const timeout = setTimeout(() => {
        showNotification('Message delivery failed. Please refresh the page and try again.', 'error')

        setMessages(prev => prev.filter(msg => msg.tempId !== tempId))
        pendingMessagesRef.current.delete(tempId)
      }, MESSAGE_RESOLVE_TIMEOUT)

      pendingMessagesRef.current.set(tempId, timeout)

      const command: CreateMessageCommand = {
        receiverId: selectedChat.id,
        body: messageBody
      }
      await connectionRef.current.invoke("SendMessage", command)
    } catch (error) {
      showNotification('Failed to send message.', 'error')

      const timeout = pendingMessagesRef.current.get(tempId)
      if (timeout) {
        clearTimeout(timeout)
        pendingMessagesRef.current.delete(tempId)
      }

      setMessages(prev => prev.filter(msg => msg.tempId != tempId))
    }
  }, [messageText, selectedChat, isConnected, userAuthData?.id, showNotification, scrollToBottom])

  const handleKeyDown = useCallback((e: React.KeyboardEvent) => {
    if (e.key === 'Enter' && !e.shiftKey) {
      e.preventDefault()
      handleSendMessage()
    }
  }, [handleSendMessage])

  const formatMessageDate = useCallback((sentTime: string) => {
    try {
      const date = new Date(sentTime)
      if (isNaN(date.getTime())) {
        return 'Invalid Date'
      }
      return date.toLocaleDateString()
    } catch {
      return 'Invalid Date'
    }
  }, [])

  const formatMessageTime = useCallback((sentTime: string) => {
    try {
      const date = new Date(sentTime)
      if (isNaN(date.getTime())) {
        return ''
      }
      return date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })
    } catch {
      return ''
    }
  }, [])

  // Group messages by date
  const groupedMessages = useMemo(() => {
    const messagesByDate: { [date: string]: OptimisticMessage[] } = {}

    messages.forEach(message => {
      if (!message.sentTime) return

      const date = formatMessageDate(message.sentTime)
      if (!messagesByDate[date]) {
        messagesByDate[date] = []
      }
      messagesByDate[date].push(message)
    })

    return messagesByDate
  }, [messages, formatMessageDate])

  const renderMessagesByDate = useCallback(() => {
    return Object.entries(groupedMessages).reverse().map(([date, dateMessages]) => (
      <div key={date}>
        <p className={styles.dateHeader}>{date}</p>
        {dateMessages.map((message, index) => (
          <div
            key={message.id || message.tempId || `${index}-${message.body}`}
            className={`${styles.messageWrapper} ${message.senderId == userAuthData?.id
              ? styles.ownMessage
              : styles.otherMessage
              }`}
          >
            <div className={`${styles.messageBubble} ${message.tempId ? styles.pending : ''}`}>
              {message.body}
              <div className={styles.messageTime}>
                {formatMessageTime(message.sentTime!)}
              </div>
            </div>
          </div>
        ))}
      </div>
    ))
  }, [groupedMessages, userAuthData?.id, formatMessageTime])

  return (
    <div className={styles.messagesBox}>
      <HomeButton />
      <div className={styles.messagesWrapper}>
        <div className={styles.chatList}>
          {loadingChats ? (
            <LoadingSpinner />
          ) : chats.length === 0 ? (
            <div className={styles.noChats}>No conversations yet...</div>
          ) : (
            <List disablePadding>
              {chats.map(chat => (
                <ListItemButton
                  sx={{ maxHeight: '69px' }}
                  key={chat.id}
                  selected={selectedChat?.id == chat.id}
                  onClick={() => handleChatSelect(chat)}
                >
                  <ListItemText
                    primary={chat.userName}
                    secondary={chat.lastMessage}
                  />
                </ListItemButton>
              ))}
            </List>
          )}
        </div>

        <div className={styles.messagePanel}>
          {!selectedChat ? (
            <div className={styles.noSelectedChat}>
              Hello! Select one of the users on the left to see your messages with them.
            </div>
          ) : (
            <>
              <div className={styles.chatHeader}>
                <b>{selectedChat.userName}</b>
                <Button
                  variant="outlined"
                  onClick={() => navigate(`/users/${selectedChat.id}`)}
                >
                  View Profile
                </Button>
              </div>
              <Divider />

              <div
                ref={messagesContainerRef}
                className={styles.messagesArea}
                onScroll={debounce(handleScroll, 200)}
              >
                {loadingMore && (
                  <div className={styles.loadingMore}>
                    <CircularProgress size={20} />
                  </div>
                )}
                {loadingMessages ? (
                  <LoadingSpinner />
                ) : messages.length === 0 ? (
                  <p className={styles.noMessages}>
                    No messages yet. Start a conversation!
                  </p>
                ) : (
                  renderMessagesByDate()
                )}
              </div>

              <div className={styles.messageInput}>
                <div className={styles.inputWrapper}>
                  <TextField
                    fullWidth
                    multiline
                    maxRows={4}
                    value={messageText}
                    onChange={event => setMessageText(event.target.value)}
                    onKeyDown={handleKeyDown}
                    placeholder="Type a message..."
                    size="small"
                    disabled={!isConnected}
                  />
                  <IconButton
                    color="primary"
                    onClick={handleSendMessage}
                    disabled={!messageText.trim() || !isConnected}
                  >
                    <SendIcon />
                  </IconButton>
                </div>
              </div>
            </>
          )}
        </div>
      </div>
    </div>
  )
}

export default Messages