import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import { useFormik } from 'formik';
import { type AuthorDto } from '../../api/generated';
import styles from './AddCustomAuthorDialog.module.css'

interface AddCustomAuthorProps {
  defaultName?: string;
  onClose: () => void;
  onEntityCreated: (author: AuthorDto) => void;
}

export const AddCustomAuthor = ({
  defaultName,
  onClose,
  onEntityCreated
}: AddCustomAuthorProps) => {
  const formik = useFormik({
    initialValues: {
      firstName: '',
      middleName: '',
      lastName: defaultName
    },
    onSubmit: (values) => {
      const newAuthor: AuthorDto = {
        id: undefined,
        firstName: values.firstName,
        middleName: values.middleName,
        lastName: values.lastName
      }
      onEntityCreated(newAuthor)
      onClose();
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
            <ul>
              <li>The first name can be omitted in case if the author is known only by a single name (e.g. Plato, Aristotle).</li>
              <li>Specify the middle name only if it's commonly spoken out when referring to the author (e.g. John Ronald Reuel Tolkien).</li>
            </ul>
            <TextField
              label="First Name"
              name="firstName"
              value={formik.values.firstName}
              onChange={formik.handleChange}
            />
            <TextField
              label="Middle Name"
              name="middleName"
              value={formik.values.middleName}
              onChange={formik.handleChange}
            />
            <TextField
              label="Last Name"
              name="lastName"
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