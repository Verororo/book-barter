import type { ListedBook } from "../../api/view-models/listed-book"
import BookItem from "../BookItem/BookItem"
import styles from './GivingOut.module.css'

type GivingOutSectionProps = {
  givingOutBooks: ListedBook[]
}

const GivingOutSection = ({ givingOutBooks }: GivingOutSectionProps) => {
  return (
    <div className={styles.givingOut}>
      <span className={styles.givingOutHeader}>
        <img src='../../public/OrangeBookGivenOut.svg' />
        <p>is giving out...</p>
      </span>
      <div className={styles.bookItemContainer}>
        {givingOutBooks.map(book => (
          <BookItem
            key={book.title}
            {...book}
          />
        ))}
      </div>
    </div>
  )
}

export default GivingOutSection