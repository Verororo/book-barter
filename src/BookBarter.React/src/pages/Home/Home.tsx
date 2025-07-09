import UserItemContainer from "../../components/UserItem/UserItemContainer"
import styles from './Home.module.css'

const Home = () => (
  <>
    <header className={styles.logo}>
      <img src="../../public/Logo.svg" alt="BookBarter logo" />
    </header>

    

    <UserItemContainer />
  </>
)

export default Home