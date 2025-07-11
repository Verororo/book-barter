import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import { useFormik } from 'formik';
import { type AuthorDto } from '../../api/generated';
import { createAuthorCommand } from '../../api/author-client';
import styles from './AddCustomAuthorDialog.module.css'

interface AddCustomAuthorProps {
  defaultName?: string;
  onClose: () => void;
  onAuthorCreated: (author: AuthorDto) => void;
}

export const AddCustomAuthor = ({
  defaultName,
  onClose,
  onAuthorCreated
}: AddCustomAuthorProps) => {
  const formik = useFormik({
    initialValues: {
      firstName: '',
      middleName: '',
      lastName: defaultName ? defaultName.split(' ').pop() || '' : '',
    },
    onSubmit: (values) => {
      createAuthorCommand({
        firstName: values.firstName || null,
        middleName: values.middleName || null,
        lastName: values.lastName,
      })
        .then(response => {
          const newAuthor: AuthorDto = {
            id: response,
            firstName: values.firstName || null,
            middleName: values.middleName || null,
            lastName: values.lastName,
          };
          onAuthorCreated(newAuthor);
          onClose();
        })
        .catch(error => {
          console.error(error);
        });
    }
  });

  return (
    <Dialog
      open
      onClose={onClose}
      fullWidth
      maxWidth="xs"
    >
      <DialogTitle>Add New Author</DialogTitle>
      <form onSubmit={formik.handleSubmit}>
        <DialogContent dividers>
          <div className={styles.dialogContents}>
            <TextField
              label="First Name (Optional)"
              name="firstName"
              value={formik.values.firstName}
              onChange={formik.handleChange}
            />
            <TextField
              label="Middle Name (Optional)"
              name="middleName"
              value={formik.values.middleName}
              onChange={formik.handleChange}
            />
            <TextField
              label="Last Name"
              name="lastName"
              required
              value={formik.values.lastName}
              onChange={formik.handleChange}
              error={!formik.values.lastName}
              helperText={!formik.values.lastName ? 'Last name is required' : ''}
            />
          </div>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose}>Cancel</Button>
          <Button
            type="submit"
            variant="contained"
            disabled={!formik.values.lastName}
          >
            Add Author
          </Button>
        </DialogActions>
      </form>
    </Dialog>
  );
};