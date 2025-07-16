import { useState } from 'react';
import {
  Accordion,
  AccordionSummary,
  AccordionDetails,
  Chip,
  Stack,
} from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import PublisherForm from './Forms/PublisherForm';
import { approvePublisher, deletePublisher } from '../../api/clients/publisher-client';
import type { PublisherForModerationDto } from '../../api/generated';
import ActionButtons from './Buttons/ActionButtons';
import styles from './List.module.css'

interface PublishersListProps {
  publishers: PublisherForModerationDto[];
  onRefresh: () => Promise<void>;
  hasPagination: boolean;
}

const PublishersList = ({ publishers, onRefresh, hasPagination }: PublishersListProps) => {
  const [editingPublisherId, setEditingPublisherId] = useState<number | null>(null);
  const [expandedId, setExpandedId] = useState<number | false>(false);

  const handleAccordionChange = (publisherId: number) => (_: any, isExpanded: boolean) => {
    setExpandedId(isExpanded ? publisherId : false);
  };

  const handlePublisherApprove = async (publisherId: number) => {
    try {
      await approvePublisher(publisherId);
      await onRefresh();
    } catch (error) {
      console.log(error);
    }
  };

  const handlePublisherDelete = async (publisherId: number) => {
    try {
      await deletePublisher(publisherId);
      await onRefresh();
    } catch (error) {
      console.log(error);
    }
  };

  const handlePublisherSave = async () => {
    setEditingPublisherId(null);
    await onRefresh();
  };

  return (
    <div>
      {publishers.map((publisher) => {
        const isEditing = editingPublisherId === publisher.id;

        return (
          <Accordion
            key={publisher.id}
            expanded={expandedId === publisher.id}
            onChange={handleAccordionChange(publisher.id!)}
            className={`${hasPagination
              ? styles.accordionItemLastWithPagination
              : styles.accordionItem
              }`}
            disableGutters
            elevation={0}
          >
            <AccordionSummary expandIcon={<ExpandMoreIcon />}>
              <div>
                <h3 className={styles.accordionTitle}>{publisher.name}</h3>
              </div>
            </AccordionSummary>
            <AccordionDetails className={styles.accordionDetails}>
              {isEditing ? (
                <PublisherForm
                  publisher={publisher}
                  onSave={handlePublisherSave}
                  onCancel={() => setEditingPublisherId(null)}
                />
              ) : (
                <div className={styles.detailsInfo}>
                  <div className={[styles.infoGrid, styles.publisherInfoGrid].join(" ")}>
                    <div className={styles.infoRow}>
                      <span className={styles.infoLabel}>Name:</span> {publisher.name}
                    </div>
                  </div>

                  <div className={styles.infoRow}>
                    <span className={styles.infoLabel}>Books:</span>
                    <Stack direction="row" spacing={1} flexWrap="wrap" mt={1}>
                      {publisher.books?.map(book => (
                        <Chip
                          key={book.id}
                          label={book.title}
                          size="small"
                        />
                      ))}
                    </Stack>
                  </div>

                  <ActionButtons
                    onDelete={() => handlePublisherDelete(publisher.id!)}
                    onEdit={() => setEditingPublisherId(publisher.id!)}
                    onApprove={!publisher.approved ? () => handlePublisherApprove(publisher.id!) : undefined}
                    isDeleteDisabled={publisher.books ? publisher.books.length > 0 : false}
                    deleteDisabledText="Deleting a publisher referred by the existing books is forbidden. Delete the referring books first."
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

export default PublishersList;