
import styles from './Navigation.module.css'
import Button from '@mui/material/Button';

const Navigation = () => (
  <nav className={styles.nav}>
    <Button variant="text" size="large">My Profile</Button>
    <Button variant="text" size="large">Messages</Button>
    <Button variant="text" size="large">Moderator Panel</Button>
  </nav>
)

export default Navigation