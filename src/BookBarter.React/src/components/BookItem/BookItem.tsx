
import styles from './BookItem.module.css';

export interface Book {
  author: string,
  bookTitle: string,
  yearPublished: number,
  publisher: string,
  state?: string
}

const BookItem = ({ author, bookTitle, yearPublished, publisher, state } : Book) => (
  <div className={styles.bookItem}>
    <div className={styles.bookItemHeader}>
      <p className={styles.bookItemAuthor}>{author}.</p>
      <p className={styles.bookItemTitle}>{bookTitle}</p>
    </div>
    <div className={styles.bookItemDescription}>
      <p className={styles.bookItemPublishedAndState}>
        {yearPublished}, {publisher}{state && `. State: ${state}`}
      </p>
    </div>
  </div>
);

export default BookItem;