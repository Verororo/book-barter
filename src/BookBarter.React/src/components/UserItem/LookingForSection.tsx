import type { ListedBook } from "../../api/view-models/listed-book"
import BookItem from "../BookItem/BookItem"
import styles from './LookingFor.module.css'

type LookingForSectionProps = {
  lookingForBooks: ListedBook[]
}

const LookingForSection = ({ lookingForBooks }: LookingForSectionProps) => {
  return (
    <div className={styles.lookingFor}>
      <span className={styles.lookingForHeader}>
        <img src='../../public/BlueBookLookedFor.svg' />
        <p>is looking for...</p>
      </span>
      <div className={styles.bookItemContainer}>
        {lookingForBooks.map(book => (
          <BookItem
            key={book.title}
            {...book}
          />
        ))}
      </div>
    </div>
  )
}

export default LookingForSection