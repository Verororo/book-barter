import { useState } from 'react';
import {
  Accordion,
  AccordionSummary,
  AccordionDetails,
  Chip,
  Stack,
} from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { approveBook, deleteBook } from '../../api/clients/book-client';
import type { BookForModerationDto } from '../../api/generated';
import ActionButtons from './Buttons/ActionButtons';
import BookForm from './Forms/BookForm';
import styles from './List.module.css';
import { useNotification } from '../../contexts/Notification/UseNotification';

interface BooksListProps {
  books: BookForModerationDto[];
  onRefresh: () => Promise<void>;
  hasPagination: boolean;
}

const BooksList = ({ books, onRefresh, hasPagination }: BooksListProps) => {
  const [editingBookId, setEditingBookId] = useState<number | null>(null);
  const [expandedId, setExpandedId] = useState<number | false>(false);

  const { showNotification } = useNotification();

  const handleAccordionChange =
    (bookId: number) => (_: any, isExpanded: boolean) => {
      setExpandedId(isExpanded ? bookId : false);
    };

  const handleBookApprove = async (bookId: number) => {
    try {
      await approveBook(bookId);
      await onRefresh();
      showNotification('Succesfully approved the book.', 'success');
    } catch (error) {
      showNotification('Failed to approve the book. Try again later.', 'error');
    }
  };

  const handleBookDelete = async (bookId: number) => {
    try {
      await deleteBook(bookId);
      await onRefresh();
      showNotification('Succesfully deleted the book.', 'success');
    } catch (error) {
      showNotification('Failed to delete the book. Try again later.', 'error');
    }
  };

  const handleBookSave = async () => {
    setEditingBookId(null);
    await onRefresh();
  };

  return (
    <div>
      {books.map((book) => {
        const isEditing = editingBookId === book.id;

        return (
          <Accordion
            key={book.id}
            expanded={expandedId === book.id}
            onChange={handleAccordionChange(book.id!)}
            className={`${
              hasPagination
                ? styles.accordionItemLastWithPagination
                : styles.accordionItem
            }`}
            disableGutters
            elevation={0}
          >
            <AccordionSummary expandIcon={<ExpandMoreIcon />}>
              <div>
                <h3 className={styles.accordionTitle}>{book.title}</h3>
                <p className={styles.accordionSubtitle}>
                  Authors:{' '}
                  {book.authors
                    ?.map(
                      (author) =>
                        `${[
                          author.firstName,
                          author.middleName,
                          author.lastName,
                        ]
                          .filter(Boolean)
                          .join(' ')}`,
                    )
                    .join(', ')}
                </p>
                <p className={styles.accordionSubtitle}>
                  Added: {new Date(book.addedDate).toLocaleDateString()}
                </p>
              </div>
            </AccordionSummary>
            <AccordionDetails className={styles.accordionDetails}>
              {isEditing ? (
                <BookForm
                  book={book}
                  onSave={handleBookSave}
                  onCancel={() => setEditingBookId(null)}
                />
              ) : (
                <div className={styles.detailsInfo}>
                  <div
                    className={[styles.infoGrid, styles.bookInfoGrid].join(' ')}
                  >
                    <div className={styles.infoRow}>
                      <span className={styles.infoLabel}>Title:</span>{' '}
                      {book.title}
                    </div>
                    <div className={styles.infoRow}>
                      <span className={styles.infoLabel}>ISBN:</span>{' '}
                      {book.isbn}
                    </div>
                    <div className={styles.infoRow}>
                      <span className={styles.infoLabel}>
                        Publication Date:
                      </span>{' '}
                      {book.publicationDate}
                    </div>
                    <div className={styles.infoRow}>
                      <span className={styles.infoLabel}>Genre:</span>{' '}
                      {book.genre?.name}
                    </div>
                    <div className={styles.infoRow}>
                      <span className={styles.infoLabel}>Publisher:</span>{' '}
                      {book.publisher?.name}
                    </div>
                  </div>

                  <div className={styles.authorsInfoRow}>
                    <span className={styles.infoLabel}>Authors:</span>
                    <Stack direction="row" spacing={1} flexWrap="wrap" mt={1}>
                      {book.authors?.map((author) => (
                        <Chip
                          key={author?.id}
                          label={[
                            author.firstName,
                            author.middleName,
                            author.lastName,
                          ]
                            .filter(Boolean)
                            .join(' ')}
                          size="small"
                        />
                      ))}
                    </Stack>
                  </div>

                  <div className={styles.infoRow}>
                    <span className={styles.infoLabel}>Owned by:</span>
                    <Stack direction="row" spacing={1} flexWrap="wrap" mt={1}>
                      {book.ownedByUsers?.map((user) => (
                        <Chip
                          key={user.user?.id}
                          label={user.user?.userName}
                          size="small"
                        />
                      ))}
                    </Stack>
                  </div>

                  <div className={styles.infoRow}>
                    <span className={styles.infoLabel}>Wanted by:</span>
                    <Stack direction="row" spacing={1} flexWrap="wrap" mt={1}>
                      {book.wantedByUsers?.map((user) => (
                        <Chip
                          key={user.user?.id}
                          label={user.user?.userName}
                          size="small"
                        />
                      ))}
                    </Stack>
                  </div>

                  <ActionButtons
                    onDelete={() => handleBookDelete(book.id!)}
                    onEdit={() => setEditingBookId(book.id!)}
                    onApprove={
                      !book.approved
                        ? () => handleBookApprove(book.id!)
                        : undefined
                    }
                  />
                </div>
              )}
            </AccordionDetails>
          </Accordion>
        );
      })}
    </div>
  );
};

export default BooksList;
