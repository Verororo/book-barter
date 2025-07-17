import { useFormik } from 'formik';
import { TextField, Grid } from '@mui/material';
import { updateAuthor } from '../../../api/clients/author-client';
import type { AuthorForModerationDto, UpdateAuthorCommand } from '../../../api/generated';
import EditActionButtons from '../Buttons/EditActionButtons';
import styles from './Form.module.css'
import { useNotification } from '../../../contexts/Notification/UseNotification';

interface AuthorFormProps {
  author: AuthorForModerationDto;
  onSave: () => void;
  onCancel: () => void;
}

const AuthorForm = ({ author, onSave, onCancel }: AuthorFormProps) => {
  const { showNotification } = useNotification();

  const formik = useFormik({
    initialValues: {
      firstName: author.firstName,
      middleName: author.middleName,
      lastName: author.lastName
    },
    onSubmit: async (values: any) => {
      try {
        const command: UpdateAuthorCommand = {
          id: author.id!,
          firstName: values.firstName || null,
          middleName: values.middleName || null,
          lastName: values.lastName
        };
        await updateAuthor(command);
        onSave();
      } catch (error) {
        showNotification("Failed to update the author. Try again later.", "error")
      }
    }
  });

  return (
    <form
      className={styles.container}
      onSubmit={formik.handleSubmit}
    >
      <Grid container spacing={2}>
        <Grid size={{ xs: 12, sm: 4 }}>
          <TextField
            fullWidth
            label="First Name"
            name="firstName"
            value={formik.values.firstName}
            onChange={formik.handleChange}
            variant="outlined"
            className={styles.input}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 4 }}>
          <TextField
            fullWidth
            label="Middle Name"
            name="middleName"
            value={formik.values.middleName}
            onChange={formik.handleChange}
            variant="outlined"
            className={styles.input}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 4 }}>
          <TextField
            fullWidth
            required
            label="Last Name"
            name="lastName"
            value={formik.values.lastName}
            onChange={formik.handleChange}
            variant="outlined"
            className={styles.input}
            error={!formik.values.lastName}
            helperText={!formik.values.lastName ? 'Last name is required' : ''}
          />
        </Grid>
      </Grid>
      <EditActionButtons
        onCancel={onCancel}
        disabled={!formik.values.lastName}
      />
    </form>
  )
}

export default AuthorForm;