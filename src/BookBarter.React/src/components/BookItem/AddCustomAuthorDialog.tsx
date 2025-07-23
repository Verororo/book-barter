import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import { useFormik } from 'formik';
import { type AuthorDto } from '../../api/generated';
import styles from './AddCustomAuthorDialog.module.css';

interface AddCustomAuthorProps {
  defaultName?: string;
  onClose: () => void;
  onEntityCreated: (author: AuthorDto) => void;
}

type CustomAuthorValues = {
  firstName?: string;
  middleName?: string;
  lastName: string;
};

type CustomAuthorErrors = {
  firstName?: string;
  middleName?: string;
  lastName?: string;
};

export const AddCustomAuthor = ({
  defaultName,
  onClose,
  onEntityCreated,
}: AddCustomAuthorProps) => {
  const formik = useFormik({
    initialValues: {
      firstName: '',
      middleName: '',
      lastName: defaultName || '',
    },
    validate: (values) => {
      const errors: CustomAuthorErrors = {};

      if (values.firstName.length > 20)
        errors.firstName = 'First name must not exceed 20 characters.';

      if (values.middleName.length > 20)
        errors.middleName = 'Middle name must not exceed 20 characters.';

      if (!values.lastName) errors.lastName = 'Last name is required.';
      else if (values.lastName.length > 20)
        errors.lastName = 'Last name must not exceed 20 characters.';

      return errors;
    },
    onSubmit: (values) => {
      const newAuthor: AuthorDto = {
        id: undefined,
        firstName: values.firstName || null,
        middleName: values.middleName || null,
        lastName: values.lastName,
      };
      onEntityCreated(newAuthor);
      onClose();
    },
  });

  const displayedErrorFields = Object.keys(formik.errors).filter(
    (field) => formik.touched[field as keyof CustomAuthorValues],
  );

  const submitDisabled = displayedErrorFields.length > 0;

  return (
    <Dialog open onClose={onClose} fullWidth maxWidth="xs">
      <DialogTitle>Add New Author</DialogTitle>
      <form onSubmit={formik.handleSubmit}>
        <DialogContent dividers>
          <div className={styles.dialogContents}>
            The first name can be omitted in case if the author is known only by
            a single name (e.g. Plato, Aristotle).
            <br />
            Specify the middle name only if it's commonly spoken out when
            referring to the author (e.g. John Ronald Reuel Tolkien).
            <TextField
              label="First Name"
              name="firstName"
              value={formik.values.firstName}
              onChange={formik.handleChange}
              onBlur={formik.handleBlur}
              variant="outlined"
              margin="normal"
              fullWidth
              error={
                formik.touched.firstName && Boolean(formik.errors.firstName)
              }
              helperText={formik.touched.firstName && formik.errors.firstName}
            />
            <TextField
              label="Middle Name"
              name="middleName"
              value={formik.values.middleName}
              onChange={formik.handleChange}
              onBlur={formik.handleBlur}
              variant="outlined"
              margin="normal"
              fullWidth
              error={
                formik.touched.middleName && Boolean(formik.errors.middleName)
              }
              helperText={formik.touched.middleName && formik.errors.middleName}
            />
            <TextField
              required
              label="Last Name"
              name="lastName"
              value={formik.values.lastName}
              onChange={formik.handleChange}
              onBlur={formik.handleBlur}
              variant="outlined"
              margin="normal"
              fullWidth
              error={formik.touched.lastName && Boolean(formik.errors.lastName)}
              helperText={formik.touched.lastName && formik.errors.lastName}
            />
          </div>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose}>Cancel</Button>
          <Button type="submit" variant="contained" disabled={submitDisabled}>
            Add Author
          </Button>
        </DialogActions>
      </form>
    </Dialog>
  );
};
