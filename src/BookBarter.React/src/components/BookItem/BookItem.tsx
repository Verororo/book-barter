
import styles from './BookItem.module.css';

export type Book = {
  authors: string
  title: string
  publicationYear: number
  publisherName: string
  bookStateName?: string
}

const BookItem = ({ authors, title, publicationYear, publisherName, bookStateName } : Book) => (
  <div className={styles.bookItem}>
    <div className={styles.bookItemHeader}>
      <p className={styles.bookItemAuthor}>{authors}.</p>
      <p className={styles.bookItemTitle}>{title}</p>
    </div>
    <div className={styles.bookItemDescription}>
      <p className={styles.bookItemPublishedAndState}>
        {publicationYear}, {publisherName}{bookStateName && `. State: ${bookStateName}`}
      </p>
    </div>
  </div>
);

export default BookItem;