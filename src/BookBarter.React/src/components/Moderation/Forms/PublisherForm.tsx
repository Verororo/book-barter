import { useFormik } from 'formik';
import { TextField } from '@mui/material';
import { updatePublisher } from '../../../api/clients/publisher-client';
import type { PublisherForModerationDto, UpdatePublisherCommand } from '../../../api/generated';
import EditActionButtons from '../Buttons/EditActionButtons';
import styles from './Form.module.css'

interface PublisherFormProps {
  publisher: PublisherForModerationDto;
  onSave: () => void;
  onCancel: () => void;
}

const PublisherForm = ({ publisher, onSave, onCancel }: PublisherFormProps) => {
  const formik = useFormik({
    initialValues: { name: publisher.name },
    onSubmit: async (values: any) => {
      try {
        const command: UpdatePublisherCommand = {
          id: publisher.id!,
          name: values.name
        };
        await updatePublisher(command);
        onSave();
      } catch (error) {
        console.log(error);
      }
    }
  });

  return (
    <form
      className={styles.container}
      onSubmit={formik.handleSubmit}
    >
      <TextField
        fullWidth
        label="Name"
        name="name"
        value={formik.values.name}
        onChange={formik.handleChange}
        variant="outlined"
        className={styles.input}
        error={!formik.values.name}
        helperText={!formik.values.name ? 'Name is required' : ''}
      />
      <EditActionButtons
        onCancel={onCancel}
        disabled={!formik.values.name}
      />
    </form>
  )
}

export default PublisherForm;