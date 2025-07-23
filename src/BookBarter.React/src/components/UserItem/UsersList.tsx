import styles from './usersList.module.css';
import GivingOutSection from './GivingOutSection';
import LookingForSection from './LookingForSection';
import Button from '@mui/material/Button';

import ChatBubbleOutlineIcon from '@mui/icons-material/ChatBubbleOutline';
import PersonIcon from '@mui/icons-material/Person';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import { useAuth } from '../../contexts/Auth/UseAuth';
import type { ListedUser } from '../../api/view-models/listed-user';
import { Link, useNavigate } from 'react-router';
import { useNotification } from '../../contexts/Notification/UseNotification';

const formatLastOnline = (isoDate: string) => {
  const date = new Date(isoDate);

  const now = new Date();
  const diffInMilliseconds = now.getTime() - date.getTime();
  const diffInHours = Math.floor(diffInMilliseconds / (1000 * 60 * 60));
  const diffInDays = Math.floor(diffInHours / 24);

  if (diffInHours < 1) {
    return 'less than 1 hour ago';
  } else if (diffInHours < 24) {
    return `${diffInHours} hour${diffInHours > 1 ? 's' : ''} ago`;
  } else {
    return `${diffInDays} day${diffInDays > 1 ? 's' : ''} ago`;
  }
};

type usersListProps = {
  users: ListedUser[];
};

const UsersList = ({ users }: usersListProps) => {
  const { isAuthenticated } = useAuth();

  const { showNotification } = useNotification();
  const navigate = useNavigate();

  return users.map((user) => {
    const handleMessage = async () => {
      try {
        navigate('/messages', {
          state: {
            selectedUser: {
              id: user!.id,
              userName: user!.userName,
            },
          },
        });
      } catch (error) {
        showNotification(
          'Failed to message the user. Try again later.',
          'error',
        );
      }
    };

    return (
      <div key={user.id} className={styles.usersList}>
        <div className={styles.usersListHeader}>
          <div className={styles.usersListHeaderLeft}>
            <p className={styles.usersListName}>
              <PersonIcon fontSize="large" />
              {user.userName}
            </p>

            <p className={styles.usersListCity}>
              <LocationOnIcon fontSize="small" />
              {user.cityNameWithCountry}
            </p>
          </div>

          <div className={styles.usersListHeaderRight}>
            <p className={styles.usersListLastOnline}>
              last online: {formatLastOnline(user.lastOnlineDate)}
            </p>

            <div className={styles.usersListHeaderButtons}>
              <Button
                variant="outlined"
                component={Link}
                to={`/users/${user.id}`}
              >
                View Profile
              </Button>

              {isAuthenticated && (
                <Button
                  variant="contained"
                  startIcon={<ChatBubbleOutlineIcon fontSize="inherit" />}
                  onClick={handleMessage}
                >
                  Message
                </Button>
              )}
            </div>
          </div>
        </div>

        <GivingOutSection givingOutBooks={user.ownedBooks} />

        <LookingForSection lookingForBooks={user.wantedBooks} />
      </div>
    );
  });
};

export default UsersList;
