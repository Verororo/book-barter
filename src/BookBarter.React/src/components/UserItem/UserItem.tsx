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
  userName: string,
  city: string,
  lastUpdateDate: Date
}

const givingOutBooks: Book[] = [
  {
    author: "Plato",
    bookTitle: "The Republic",
    yearPublished: 2012,
    publisher: "Penguin Classics",
    state: "Medium"
  },
  {
    author: "Zorich",
    bookTitle: "Mathematical Analysis. Part I",
    yearPublished: 2019,
    publisher: "MCCME",
    state: "New"
  }
]

const lookingForBooks: Book[] = [
  {
    author: "Kant",
    bookTitle: "Critique of Pure Reason",
    yearPublished: 2007,
    publisher: "Penguin Classics"
  },
  {
    author: "Tolstoi",
    bookTitle: "War and Peace",
    yearPublished: 2005,
    publisher: "T. Egerton"
  },
  {
    author: "Dick",
    bookTitle: "Do Android Dream of Electric Sheep?",
    yearPublished: 1996,
    publisher: "Del Rey"
  }
]

function formatLastUpdate(date: Date) {
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
            {user.city}
          </p>
        </div>

        <div className={styles.userItemHeaderRight}>
          <p className={styles.userItemLastUpdate}>
            last update: {formatLastUpdate(user.lastUpdateDate)}
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

      <GivingOutSection givingOutBooks={givingOutBooks} />

      <LookingForSection lookingForBooks={lookingForBooks} />
    </div>
  )
}

export default UserItem