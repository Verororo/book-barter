import type { ListedBook } from '../../api/view-models/listed-book';
import styles from './BookItem.module.css';

export type BookItemProps = {
  listedBook: ListedBook;
  onBookDeleted?: (id: number) => void;
};

const BookItem = ({ listedBook, onBookDeleted }: BookItemProps) => {
  const { id, authors, title, publicationYear, publisherName, bookStateName } =
    listedBook;

  const handleDelete = () => {
    if (onBookDeleted) {
      onBookDeleted(id);
    }
  };

  return (
    <div className={styles.bookItem}>
      {onBookDeleted && (
        <button
          className={styles.deleteButton}
          onClick={handleDelete}
          aria-label="Remove book"
        >
          <span>✕</span>
        </button>
      )}

      <div className={styles.bookItemHeader}>
        <p className={styles.bookItemAuthor}>{authors}.</p>
        <p className={styles.bookItemTitle}>{title}</p>
      </div>
      <div className={styles.bookItemDescription}>
        <p className={styles.bookItemPublishedAndState}>
          {publicationYear}, {publisherName}
          {bookStateName && `. State: ${bookStateName}`}
        </p>
      </div>
    </div>
  );
};

export default BookItem;
