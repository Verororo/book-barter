import styles from './UserProfile.module.css'
import GivingOutSection from '../../components/UserItem/GivingOutSection'
import LookingForSection from '../../components/UserItem/LookingForSection'
import Button from '@mui/material/Button'

import ChatBubbleOutlineIcon from '@mui/icons-material/ChatBubbleOutline'
import PersonIcon from '@mui/icons-material/Person'
import LocationOnIcon from '@mui/icons-material/LocationOn'
import { useAuth } from '../../contexts/Auth/UseAuth'
import { useEffect, useState } from 'react'
import { fetchUserById } from '../../api/clients/user-client'
import { useParams } from 'react-router'
import type { User } from '../../api/view-models/user'
import LoadingSpinner from '../../components/LoadingSpinner/LoadingSpinner'
import HomeButton from '../../components/Navigation/HomeButton'

const formatLastOnline = (isoDate: string) => {
  const date = new Date(isoDate)
  const now = new Date()
  const diffMs = now.getTime() - date.getTime()
  const diffHrs = Math.floor(diffMs / (1000 * 60 * 60))
  const diffDays = Math.floor(diffHrs / 24)

  if (diffHrs < 1) {
    return 'less than 1 hour ago'
  } else if (diffHrs < 24) {
    return `${diffHrs} hour${diffHrs > 1 ? 's' : ''} ago`
  } else {
    return `${diffDays} day${diffDays > 1 ? 's' : ''} ago`
  }
}

const UserProfile = () => {
  const { isAuthenticated } = useAuth()

  let params = useParams()
  const userId = Number(params.userId)

  const [user, setUser] = useState<User>()
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    setLoading(true)
    fetchUserById(userId, /* excludeUnapprovedBooks = */ true)
      .then(user => setUser(user))
      .catch(console.error)
      .finally(() => setLoading(false))
  }, [userId])

  if (loading || !user) {
    return (
      <div className={styles.profileContainer}>
        <LoadingSpinner />
      </div>
    )
  }

  return (
    <div className={styles.profileContainer}>
      <div className={styles.profile}>
        <HomeButton />
        <div className={styles.profileHeader}>
          <div className={styles.profileHeaderLeft}>
            <p className={styles.profileName}>
              <PersonIcon fontSize="large" />
              {user.userName}
            </p>
            <p className={styles.profileCity}>
              <LocationOnIcon fontSize="small" />
              {user.cityNameWithCountry}
            </p>
          </div>

          <div className={styles.profileHeaderRight}>
            <p className={styles.profileLastOnline}>
              last online: {formatLastOnline(user.lastOnlineDate)}
            </p>

            <div className={styles.profileHeaderButtons}>
              {isAuthenticated && (
                <Button
                  variant="contained"
                  startIcon={<ChatBubbleOutlineIcon fontSize="inherit" />}
                >
                  Message
                </Button>
              )}
            </div>
          </div>
        </div>

        <div className={styles.profileAbout}>
          <b>About</b>
          {user.about 
            ? <p className={styles.aboutParagraph}>{user.about}</p>
            : <p className={styles.aboutParagraph}>This user hasn't written about himself yet...</p>}
        </div>

        <GivingOutSection givingOutBooks={user.ownedBooks} />
        <LookingForSection lookingForBooks={user.wantedBooks} />
      </div>
    </div>
  )
}

export default UserProfile
