import styles from './UserItem.module.css'
import type { Book } from '../BookItem/BookItem'
import GivingOutSection from './GivingOutSection'
import LookingForSection from './LookingForSection'
import Button from '@mui/material/Button'

import ChatBubbleOutlineIcon from '@mui/icons-material/ChatBubbleOutline';
import PersonIcon from '@mui/icons-material/Person';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import { useAuth } from '../../contexts/Auth/UseAuth'

export type User = {
  userName: string
  cityName: string
  lastOnlineDate: string
  ownedBooks: OwnedBookDto[]
  wantedBooks: WantedBookDto[]
}

type AuthorDto = { firstName: string; middleName?: string; lastName: string }
type BookDto = {
  id: number
  title: string
  publicationDate: string    // e.g. "2025-07-04"
  approved: boolean
  genreName: string
  publisherName: string
  authors: AuthorDto[]
}
type OwnedBookDto = {
  id: number
  book: BookDto
  bookStateName: string
  addedDate: string
}
type WantedBookDto = {
  id: number
  book: BookDto
  addedDate: string
}

const dtoToView = (dto: OwnedBookDto | WantedBookDto): Book => {
  const { book, bookStateName } = dto as OwnedBookDto
  // concatenate authors:
  const authors = book.authors
    .map(a => a.lastName).join(', ')

  return {
    title: book.title,
    authors,
    publicationYear: new Date(book.publicationDate).getFullYear(),
    publisherName: book.publisherName,
    bookStateName: 'bookStateName' in dto ? dto.bookStateName : undefined
  }
}

const formatLastOnline = (isoDate: string) => {
  const date = new Date(isoDate)

  const now = new Date()
  const diffInMilliseconds = now.getTime() - date.getTime()
  const diffInHours = Math.floor(diffInMilliseconds / (1000 * 60 * 60))
  const diffInDays = Math.floor(diffInHours / 24)

  if (diffInHours < 1) {
    return "less than 1 hour ago"
  } else if (diffInHours < 24) {
    return `${diffInHours} hour${diffInHours > 1 ? 's' : ''} ago`
  } else {
    return `${diffInDays} day${diffInDays > 1 ? 's' : ''} ago`
  }
}

type UserItemProps = {
  user: User
}

const UserItem = ({ user }: UserItemProps) => {
  const { isAuthenticated } = useAuth();

  return (
    <div className={styles.userItem}>
      <div className={styles.userItemHeader}>
        <div className={styles.userItemHeaderLeft}>
          <p className={styles.userItemName}>
            <PersonIcon fontSize='large' />
            {user.userName}
          </p>

          <p className={styles.userItemCity}>
            <LocationOnIcon fontSize='small' />
            {user.cityName}
          </p>
        </div>

        <div className={styles.userItemHeaderRight}>
          <p className={styles.userItemLastUpdate}>
            last update: {formatLastOnline(user.lastOnlineDate)}
          </p>

          <div className={styles.userItemHeaderButtons}>
            <Button variant='outlined'>
              View Profile
            </Button>


            {isAuthenticated && (
              <Button
                variant='contained'
                startIcon={
                  <ChatBubbleOutlineIcon fontSize="inherit" />
                }>
                Message
              </Button>
            )}
          </div>
        </div>
      </div>

      <GivingOutSection givingOutBooks={user.ownedBooks.map(dtoToView)} />

      <LookingForSection lookingForBooks={user.wantedBooks.map(dtoToView)} />
    </div>
  )
}

export default UserItem