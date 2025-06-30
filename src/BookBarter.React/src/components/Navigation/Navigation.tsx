// Navigation.tsx
import styles from './Navigation.module.css'
import Button from '@mui/material/Button';

import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import ForumIcon from '@mui/icons-material/Forum';
import BuildIcon from '@mui/icons-material/Build';
import { Link } from 'react-router-dom';
import { useAuth } from '../../contexts/Auth/UseAuth';

const Navigation = () => {
  const { isAuthenticated, user, logout } = useAuth();

  return (
    <nav className={styles.nav}>
      {isAuthenticated ? (
        <Button
          variant="text"
          size="large"
          startIcon={<AccountCircleIcon />}
          onClick={logout}
        >
          {user?.userName} (Logout)
        </Button>
      ) : (
        <Button
          component={Link}
          to="/auth"
          variant="text"
          size="large"
          startIcon={<AccountCircleIcon />}
        >
          My Profile
        </Button>
      )}

      {isAuthenticated && (
        <Button
          variant="text"
          size="large"
          startIcon={<ForumIcon />}
        >
          Messages
        </Button>
      )}

      {isAuthenticated && (user?.role === 'Moderator' || user?.role === 'Admin') && (
        <Button
          variant="text"
          size="large"
          startIcon={<BuildIcon />}
        >
          Moderator Panel
        </Button>
      )}
    </nav>
  );
};

export default Navigation;