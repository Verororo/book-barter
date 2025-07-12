import { useState, useEffect } from 'react'; // Added useEffect
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import { useFormik } from 'formik';
import { type PublisherDto, type AuthorDto, type GenreDto, type CreateBookCommand, type ListedBookDto } from '../../api/generated';
import SingleSearchBar from '../SearchBars/SingleSearchBar';
import styles from './AddCustomBookDialog.module.css';
import { fetchPagedGenres } from '../../api/clients/genre-client';
import { fetchPagedPublishers } from '../../api/clients/publisher-client';
import { fetchPagedAuthors } from '../../api/clients/author-client';
import { MenuItem, Select, FormControl, InputLabel } from '@mui/material'; // Added FormControl components
import { createBookCommand } from '../../api/clients/book-client';
import MultipleSearchBarWithAdd from '../SearchBars/AuthorsSearchBar';
import AuthorsSearchBar from '../SearchBars/AuthorsSearchBar';

type CustomBookValues = {
  isbn: string
  title: string
  publicationDate: string
  authors: AuthorDto[]
  genre: GenreDto | null
  publisher: PublisherDto | null
}

type CustomBookErrors = {
  isbn?: string
  title?: string
  publicationDate?: string
  authors?: string
  genre?: string
  publisher?: string
}

interface AddCustomBookProps {
  defaultTitle?: string
  onClose: () => void
  onBookCreated: (listedBook: ListedBookDto) => void
}

export const AddCustomBook = ({ defaultTitle = '', onClose, onBookCreated }: AddCustomBookProps) => {
  const [genres, setGenres] = useState<GenreDto[]>([]);

  useEffect(() => {
    fetchPagedGenres('')
      .then((response) => {
        setGenres(response);
      })
      .catch(error => console.log(error));
  }, []);

  const validate = (values: CustomBookValues) => {
    const errors: CustomBookErrors = {};

    if (!values.isbn) errors.isbn = 'ISBN is required.';
    else if (!/^\d+$/.test(values.isbn)) errors.isbn = 'ISBN must contain only digits (0-9).';
    else if (values.isbn.length != 13) errors.isbn = 'ISBN length should be 13.';

    if (!values.title) errors.title = 'Title is required.';
    else if (values.title.length > 100) errors.title = 'Title length cannot exceed 100 characters.';

    if (!values.publicationDate) errors.publicationDate = 'Publication date is required.';

    if (!values.authors) errors.genre = 'Author is required.';

    if (!values.genre || !values.genre.id) errors.genre = 'Genre is required.';

    if (!values.publisher || !values.publisher.id) errors.publisher = 'Publisher is required.';

    return errors;
  }

  const formik = useFormik<CustomBookValues>({
    initialValues: {
      isbn: '',
      title: defaultTitle,
      publicationDate: new Date().toISOString().split('T')[0],
      authors: [],
      genre: null,
      publisher: null,
    },
    validate,
    onSubmit: (values) => {
      const command: CreateBookCommand = {
        isbn: values.isbn,
        title: values.title,
        publicationDate: values.publicationDate,
        authorsIds: values.authors.map(author => author.id!),
        genreId: values.genre?.id!,
        publisherId: values.publisher?.id!
      }

      createBookCommand(command)
        .then(response => {

          const newListedBook : ListedBookDto = {
            id: response,
            title: values.title,
            publicationDate: values.publicationDate,
            genreName: values.genre?.name!,
            publisherName: values.publisher?.name!,
            authors: values.authors
          }

          response ? onBookCreated(newListedBook) : alert("Failed to add new book.");
          onClose();
        })
        .catch(error => {
          console.error(error);
        });
    }
  });

  return (
    <Dialog open onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>Add New Book</DialogTitle>
      <form onSubmit={formik.handleSubmit}>
        <DialogContent dividers>
          <div className={styles.dialogContents}>
            <TextField
              label="ISBN"
              id="isbn"
              name="isbn"
              value={formik.values.isbn}
              onChange={formik.handleChange}
              onBlur={formik.handleBlur}
              error={formik.touched.isbn && Boolean(formik.errors.isbn)}
              helperText={formik.touched.isbn && formik.errors.isbn}
            />

            <TextField
              label="Title"
              id="title"
              name="title"
              value={formik.values.title}
              onChange={formik.handleChange}
              onBlur={formik.handleBlur}
              error={formik.touched.title && Boolean(formik.errors.title)}
              helperText={formik.touched.title && formik.errors.title}
            />

            <TextField
              label="Publication Date"
              type="date"
              id="publicationDate"
              name="publicationDate"
              value={formik.values.publicationDate}
              onChange={formik.handleChange}
              onBlur={formik.handleBlur}
              error={formik.touched.publicationDate && Boolean(formik.errors.publicationDate)}
              helperText={formik.touched.publicationDate && formik.errors.publicationDate}
            />

            <AuthorsSearchBar
              id="authors"
              label="Authors"
              value={formik.values.authors}
              getOptionLabel={(option) =>
                [option.firstName, option.middleName, option.lastName]
                  .filter(Boolean)
                  .join(' ')
              }
              onChange={(_event, newValue) =>
                formik.setFieldValue("authors", newValue)
              }
              fetchMethod={fetchPagedAuthors}
              styles={styles}
            />

            <SingleSearchBar
              id="publisher"
              label="Publisher"
              value={formik.values.publisher}
              getOptionLabel={option => option.name}
              onChange={(_event, newValue) => formik.setFieldValue("publisher", newValue)}
              fetchMethod={fetchPagedPublishers}
              styles={styles}
            />

            <FormControl>
              <InputLabel id="genre-label">Genre</InputLabel>
              <Select
                id="genre"
                label="Genre"
                labelId="genre-label"
                value={formik.values.genre?.id || ''}
                onChange={(event) => {
                  const genreId = event.target.value;
                  const selectedGenre = genres.find(g => g.id === genreId) || null;
                  formik.setFieldValue("genre", selectedGenre);
                }}
                onBlur={formik.handleBlur}
              >
                {genres.map(genre => (
                  <MenuItem key={genre.id} value={genre.id}>
                    {genre.name}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          </div>
        </DialogContent>

        <DialogActions>
          <Button onClick={onClose}>Cancel</Button>
          <Button type="submit" variant="contained">Add Book</Button>
        </DialogActions>
      </form>
    </Dialog>
  )
}