import { Button, Tooltip } from '@mui/material';
import styles from './ActionButtons.module.css'

interface ActionButtonsProps {
  onDelete: () => void;
  onEdit: () => void;
  onApprove?: () => void;
  isDeleteDisabled?: boolean;
  deleteDisabledText?: string;
}

const ActionButtons = ({
  onDelete,
  onEdit,
  onApprove,
  isDeleteDisabled = false,
  deleteDisabledText
}: ActionButtonsProps) => {
  return (
    <div className={styles.actionButtons}>
      {isDeleteDisabled ? (
        <Tooltip title={deleteDisabledText || ""} arrow>
          <span>
            <Button
              variant="contained"
              color="error"
              onClick={onDelete}
              disabled={true}
            >
              Delete
            </Button>
          </span>
        </Tooltip>
      ) : (
        <Button
          variant="contained"
          color="error"
          onClick={onDelete}
        >
          Delete
        </Button>
      )}
      <Button
        variant="contained"
        color="primary"
        onClick={onEdit}
      >
        Edit
      </Button>

      {onApprove && (
        <Button
          variant="contained"
          color="success" 
          onClick={onApprove}
        >
          Approve
        </Button>
      )}
    </div>
  );
};

export default ActionButtons;