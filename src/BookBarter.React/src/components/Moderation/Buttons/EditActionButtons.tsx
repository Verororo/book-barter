import { Button } from "@mui/material";
import styles from './EditActionButtons.module.css'

interface ActionButtonsProps {
  onCancel: () => void;
  disabled?: boolean;
  loading?: boolean;
}

const EditActionButtons = ({ onCancel, disabled = false, loading = false }: ActionButtonsProps) => {
  return (
    <div className={styles.editActionButtons}>
      <Button
        variant="outlined"
        onClick={onCancel}
      >
        Cancel
      </Button>
      <Button
        type="submit"
        variant="contained"
        color="primary"
        disabled={disabled}
        loading={loading}
      >
        Save
      </Button>
    </div>
  );
};

export default EditActionButtons;