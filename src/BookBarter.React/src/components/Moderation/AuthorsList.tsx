import { useState } from 'react';
import {
  Accordion,
  AccordionSummary,
  AccordionDetails,
  Chip,
  Stack,
} from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { approveAuthor, deleteAuthor } from '../../api/clients/author-client';
import type { AuthorForModerationDto } from '../../api/generated';
import ActionButtons from './Buttons/ActionButtons';
import AuthorForm from './Forms/AuthorForm';
import styles from './List.module.css';
import { useNotification } from '../../contexts/Notification/UseNotification';

interface AuthorsListProps {
  authors: AuthorForModerationDto[];
  onRefresh: () => Promise<void>;
  hasPagination: boolean;
}

const AuthorsList = ({
  authors,
  onRefresh,
  hasPagination,
}: AuthorsListProps) => {
  const [editingAuthorId, setEditingAuthorId] = useState<number | null>(null);
  const [expandedId, setExpandedId] = useState<number | false>(false);

  const { showNotification } = useNotification();

  const handleAccordionChange =
    (authorId: number) => (_: any, isExpanded: boolean) => {
      setExpandedId(isExpanded ? authorId : false);
    };

  const handleAuthorApprove = async (authorId: number) => {
    try {
      await approveAuthor(authorId);
      showNotification('Succesfully approved the author.', 'success');
      await onRefresh();
    } catch (error) {
      showNotification(
        'Failed to approve the author. Try again later.',
        'error',
      );
    }
  };

  const handleAuthorDelete = async (authorId: number) => {
    try {
      await deleteAuthor(authorId);
      showNotification('Succesfully deleted the author.', 'success');
      await onRefresh();
    } catch (error) {
      showNotification(
        'Failed to delete the author. Try again later.',
        'error',
      );
    }
  };

  const handleAuthorSave = async () => {
    setEditingAuthorId(null);
    await onRefresh();
  };

  return (
    <div>
      {authors.map((author) => {
        const isEditing = editingAuthorId === author.id;

        return (
          <Accordion
            key={author.id}
            expanded={expandedId === author.id}
            onChange={handleAccordionChange(author.id!)}
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
                <h3 className={styles.accordionTitle}>
                  {author.firstName} {author.middleName || ''} {author.lastName}
                </h3>
              </div>
            </AccordionSummary>
            <AccordionDetails className={styles.accordionDetails}>
              {isEditing ? (
                <AuthorForm
                  author={author}
                  onSave={handleAuthorSave}
                  onCancel={() => setEditingAuthorId(null)}
                />
              ) : (
                <div className={styles.detailsInfo}>
                  <div
                    className={[styles.infoGrid, styles.authorInfoGrid].join(
                      ' ',
                    )}
                  >
                    <div className={styles.infoRow}>
                      <span className={styles.infoLabel}>First Name:</span>{' '}
                      {author.firstName ?? ''}
                    </div>
                    <div className={styles.infoRow}>
                      <span className={styles.infoLabel}>Middle Name:</span>{' '}
                      {author.middleName ?? ''}
                    </div>
                    <div className={styles.infoRow}>
                      <span className={styles.infoLabel}>Last Name:</span>{' '}
                      {author.lastName}
                    </div>
                  </div>

                  <div className={styles.infoRow}>
                    <span className={styles.infoLabel}>Books:</span>
                    <Stack direction="row" spacing={1} flexWrap="wrap" mt={1}>
                      {author.books?.map((book) => (
                        <Chip key={book.id} label={book.title} size="small" />
                      ))}
                    </Stack>
                  </div>

                  <ActionButtons
                    onDelete={() => handleAuthorDelete(author.id!)}
                    onEdit={() => setEditingAuthorId(author.id!)}
                    onApprove={
                      !author.approved
                        ? () => handleAuthorApprove(author.id!)
                        : undefined
                    }
                    isDeleteDisabled={
                      author.books ? author.books.length > 0 : false
                    }
                    deleteDisabledText="Deleting an author referred by the existing books is forbidden. Delete the referring books first."
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

export default AuthorsList;
