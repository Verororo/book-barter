import styles from './UserItem.module.css'
import GivingOutSection from './GivingOutSection'
import LookingForSection from './LookingForSection'
import Button from '@mui/material/Button'

import ChatBubbleOutlineIcon from '@mui/icons-material/ChatBubbleOutline';
import PersonIcon from '@mui/icons-material/Person';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import { useAuth } from '../../contexts/Auth/UseAuth'
import type { ListedUser } from '../../api/view-models/listed-user'

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
  user: ListedUser
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
          <p className={styles.userItemLastOnline}>
            last online: {formatLastOnline(user.lastOnlineDate)}
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

      <GivingOutSection givingOutBooks={user.ownedBooks} />

      <LookingForSection lookingForBooks={user.wantedBooks} />
    </div>
  )
}

export default UserItem