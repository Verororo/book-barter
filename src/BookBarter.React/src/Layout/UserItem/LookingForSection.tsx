import BookItem from "../BookItem/BookItem"
import type { Book } from '../BookItem/BookItem'
import styles from './LookingFor.module.css'

type LookingForSectionProps = {
  lookingForBooks: Book[]
}

const LookingForSection = ({ lookingForBooks }: LookingForSectionProps) => {
  return (
    <div className={styles.lookingFor}>
      <p>
        is looking for...
      </p>
      <div className={styles.bookItemContainer}>
        {lookingForBooks.map(book => (
          <BookItem
            key={book.bookTitle}
            {...book}
          />
        ))}
      </div>
    </div>
  )
}

export default LookingForSection