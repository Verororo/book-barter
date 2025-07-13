import { motion } from 'framer-motion';
import type { ListedBook } from '../../api/view-models/listed-book';
import styles from './BookItem.module.css';
import CloseIcon from '@mui/icons-material/Close';

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
    <motion.div
      className={styles.bookItem}
      initial={{ scale: 0 }}
      animate={{ scale: 1 }}
      exit={{ scale: 0 }}
    >
      {onBookDeleted && (
        <button
          className={styles.deleteButton}
          onClick={handleDelete}
          aria-label="Remove book"
        >
          <CloseIcon sx={{ fontSize: '16px' }} />
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
    </motion.div>
  );
};

export default BookItem;
