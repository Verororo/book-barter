
import type { ListedBook } from '../../api/view-models/listed-book';
import styles from './BookItem.module.css';

const BookItem = ({ authors, title, publicationYear, publisherName, bookStateName } : ListedBook) => (
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