import { motion } from 'framer-motion';
import type { ListedBook } from '../../api/view-models/listed-book';
import styles from './BookItem.module.css';
import CloseIcon from '@mui/icons-material/Close';
import { Tooltip } from '@mui/material';

export type BookItemProps = {
  book: ListedBook;
  onBookDeleted?: (id: number) => void;
};

const BookItem = ({ book, onBookDeleted }: BookItemProps) => {
  const handleDelete = () => {
    if (onBookDeleted) {
      onBookDeleted(book.id);
    }
  };

  const getBackgroundStyle = () => {
    if (book.color === 'orange') return styles.bookItemOrange;
    if (book.color === 'blue') return styles.bookItemBlue;
    return '';
  };

  return (
    <Tooltip title={
      book.approved
        ? ""
        : "This book is currently being reviewed by the moderator. It's visible only to you and will appear publicly in your profile after it's validated."
    } arrow
    >
      <motion.div
        className={`${book.approved ? styles.bookItem : styles.unapprovedBookItem} ${getBackgroundStyle()}`}
        initial={{ scale: 0 }}
        animate={{ scale: 1 }}
        exit={{ opacity: 0, scale: 0 }}
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
          <p className={styles.bookItemAuthor}>{book.authors}.</p>
          <p className={styles.bookItemTitle}>{book.title}</p>
        </div>
        <div className={styles.bookItemDescription}>
          <p className={styles.bookItemPublishedAndState}>
            {book.publicationYear}, {book.publisherName}
            {book.bookStateName && `. State: ${book.bookStateName}`}
          </p>
        </div>
      </motion.div>
    </Tooltip>
  );
};

export default BookItem;
