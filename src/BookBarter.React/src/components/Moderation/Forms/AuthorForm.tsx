import { useFormik } from 'formik';
import { TextField, Grid } from '@mui/material';
import { updateAuthor } from '../../../api/clients/author-client';
import type {
  AuthorForModerationDto,
  UpdateAuthorCommand,
} from '../../../api/generated';
import EditActionButtons from '../Buttons/EditActionButtons';
import styles from './Form.module.css';
import { useNotification } from '../../../contexts/Notification/UseNotification';

interface AuthorFormProps {
  author: AuthorForModerationDto;
  onSave: () => void;
  onCancel: () => void;
}

type UpdateAuthorValues = {
  firstName?: string;
  middleName?: string;
  lastName: string;
};

type UpdateAuthorErrors = {
  firstName?: string;
  middleName?: string;
  lastName?: string;
};

const AuthorForm = ({ author, onSave, onCancel }: AuthorFormProps) => {
  const { showNotification } = useNotification();

  const formik = useFormik<UpdateAuthorValues>({
    initialValues: {
      firstName: author.firstName || '',
      middleName: author.middleName || '',
      lastName: author.lastName || '',
    },
    validate: (values) => {
      const errors: UpdateAuthorErrors = {};

      if (values.firstName && values.firstName.length > 20)
        errors.firstName = 'First name must not exceed 20 characters.';

      if (values.middleName && values.middleName.length > 20)
        errors.middleName = 'Middle name must not exceed 20 characters.';

      if (!values.lastName) errors.lastName = 'Last name is required.';
      else if (values.lastName.length > 20)
        errors.lastName = 'Last name must not exceed 20 characters.';

      return errors;
    },
    onSubmit: async (values: any) => {
      try {
        const command: UpdateAuthorCommand = {
          id: author.id!,
          firstName: values.firstName || null,
          middleName: values.middleName || null,
          lastName: values.lastName,
        };
        await updateAuthor(command);
        showNotification('Successfully updated the author.', 'success');
        onSave();
      } catch (error) {
        showNotification(
          'Failed to update the author. Try again later.',
          'error',
        );
      }
    },
  });

  const displayedErrorFields = Object.keys(formik.errors).filter(
    (field) => formik.touched[field as keyof UpdateAuthorValues],
  );

  const submitDisabled = displayedErrorFields.length > 0;

  return (
    <form className={styles.container} onSubmit={formik.handleSubmit}>
      <Grid container spacing={2}>
        <Grid size={{ xs: 12, sm: 4 }}>
          <TextField
            fullWidth
            label="First Name"
            name="firstName"
            value={formik.values.firstName}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            variant="outlined"
            className={styles.input}
            error={Boolean(formik.errors.firstName)}
            helperText={formik.errors.firstName}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 4 }}>
          <TextField
            fullWidth
            label="Middle Name"
            name="middleName"
            value={formik.values.middleName}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            variant="outlined"
            className={styles.input}
            error={Boolean(formik.errors.middleName)}
            helperText={formik.errors.middleName}
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
            onBlur={formik.handleBlur}
            variant="outlined"
            className={styles.input}
            error={Boolean(formik.errors.lastName)}
            helperText={formik.errors.lastName}
          />
        </Grid>
      </Grid>
      <EditActionButtons onCancel={onCancel} disabled={submitDisabled} />
    </form>
  );
};

export default AuthorForm;
