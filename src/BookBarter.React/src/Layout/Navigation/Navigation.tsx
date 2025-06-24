
import styles from './Navigation.module.css'

const Navigation = () => (
  <nav className={styles.nav}>
    <button className={styles.button}>My Profile</button>
    <button className={styles.button}>Messages</button>
    <button className={styles.button}>Moderator Panel</button>
  </nav>
)

export default Navigation