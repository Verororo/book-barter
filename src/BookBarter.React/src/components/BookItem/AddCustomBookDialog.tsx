import { useState, useEffect, useMemo, useCallback, type FormEvent } from 'react'; // Added useEffect
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import { useFormik } from 'formik';
import { type PublisherDto, type AuthorDto, type GenreDto, type CreateBookCommand, type ListedBookDto } from '../../api/generated';
import styles from './AddCustomBookDialog.module.css';
import { fetchPagedGenres } from '../../api/clients/genre-client';
import { createPublisherCommand, fetchPagedPublishers } from '../../api/clients/publisher-client';
import { createAuthorCommand, fetchPagedAuthors } from '../../api/clients/author-client';
import { MenuItem, Select, FormControl, InputLabel, FormHelperText } from '@mui/material';
import { createBookCommand } from '../../api/clients/book-client';
import { AddCustomAuthor } from './AddCustomAuthorDialog';
import MultipleSearchBarWithCustom from '../SearchBars/MultipleSearchBarWithCustom';
import SingleSearchBarWithCustom from '../SearchBars/SingleSearchBarWithCustom';

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
  const [loading, setLoading] = useState(false);
  const [genres, setGenres] = useState<GenreDto[]>([]);

  useEffect(() => {
    fetchPagedGenres('')
      .then((response) => {
        setGenres(response);
      })
      .catch(error => console.log(error));
  }, []);

  const validate = useCallback((values: CustomBookValues) => {
    const errors: CustomBookErrors = {};

    if (!values.isbn) errors.isbn = 'ISBN is required.';
    else if (!/^\d+$/.test(values.isbn)) errors.isbn = 'ISBN must contain only digits.';
    else if (values.isbn.length !== 13) errors.isbn = 'ISBN length should be 13.';

    if (!values.title) errors.title = 'Title is required.';
    else if (values.title.length > 100) errors.title = 'Title cannot exceed 100 characters.';

    if (!values.publicationDate) errors.publicationDate = 'Publication date is required.';

    if (values.authors.length === 0) errors.authors = 'At least one author is required.';

    if (!values.genre?.id) errors.genre = 'Genre is required.';

    if (!values.publisher) errors.publisher = 'Publisher is required.';

    return errors;
  }, []);

  async function resolveAuthorsIds(authors: AuthorDto[]): Promise<AuthorDto[]> {
    return Promise.all(
      authors.map(async author => {
        if (author.id) return author;

        const newId = await createAuthorCommand({
          firstName: author.firstName,
          middleName: author.middleName,
          lastName: author.lastName!,
        });
        return { ...author, id: newId };
      })
    );
  }

  async function resolvePublisherId(publisher: PublisherDto): Promise<PublisherDto> {
    if (publisher.id) return publisher;

    const newId = await createPublisherCommand({
      name: publisher.name,
    });
    return { ...publisher, id: newId };
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
    validateOnChange: false,
    validateOnBlur: true,
    onSubmit: async (values) => {
      setLoading(true);

      try {
        const [completeAuthors, completePublisher] = await Promise.all([
          resolveAuthorsIds(values.authors),
          resolvePublisherId(values.publisher!)
        ]);

        const command: CreateBookCommand = {
          isbn: values.isbn,
          title: values.title,
          publicationDate: values.publicationDate,
          authorsIds: completeAuthors.map(a => a.id!),
          genreId: values.genre?.id!,
          publisherId: completePublisher?.id!
        }

        const newBookId = await createBookCommand(command);
        const newListedBook: ListedBookDto = {
          id: newBookId,
          title: values.title,
          publicationDate: values.publicationDate,
          genreName: values.genre?.name!,
          publisherName: completePublisher?.name!,
          authors: completeAuthors
        }

        onBookCreated(newListedBook)
        onClose()
      }
      catch (error) {
        console.log(error)
      }
      finally {
        setLoading(false)
      }
    }
  })

  const onAuthorsChange = useCallback(
    (_event: any, newValue: AuthorDto[]) => {
      formik.setFieldValue("authors", newValue, /* shouldValidate = */ true);

      formik.setFieldTouched("authors", true, /* shouldValidate = */ false);
    },
    [formik]
  );

  const onPublisherChange = useCallback(
    (_event: any, newValue: any) => {
      formik.setFieldValue("publisher", newValue, /* shouldValidate = */ true);

      formik.setFieldTouched("publisher", true, /* shouldValidate = */ false);
    },
    [formik]
  );

  const genreOptions = useMemo(() =>
    genres.map(g => (
      <MenuItem key={g.id} value={g.id}>
        {g.name}
      </MenuItem>
    )),
    [genres]
  );

  const allFields: Array<keyof CustomBookValues> = [
    'isbn', 'title', 'publicationDate', 'authors', 'genre', 'publisher'
  ];

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    if (Object.keys(formik.touched).length === 0) {
      allFields.forEach(field => {
        formik.setFieldTouched(field, true, false);
      });
    }
    formik.handleSubmit(e);
  };

  const displayedErrorFields = Object
    .keys(formik.errors)
    .filter(field => formik.touched[field as keyof CustomBookValues]);

  const submitDisabled = displayedErrorFields.length > 0;

  return (
    <Dialog open onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>Add New Book</DialogTitle>
      <form onSubmit={handleSubmit}>
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

            <MultipleSearchBarWithCustom
              id="authors"
              label="Authors"
              value={formik.values.authors}
              getOptionLabel={(option) =>
                [option.firstName, option.middleName, option.lastName]
                  .filter(Boolean)
                  .join(' ')
              }
              onChange={onAuthorsChange}
              fetchMethod={fetchPagedAuthors}
              AddDialog={AddCustomAuthor}
              styles={styles}
              error={formik.touched.authors && Boolean(formik.errors.authors)}
              helperText={formik.touched.authors && formik.errors.authors}
              onBlur={() => formik.setFieldTouched('authors', true)}
            />

            <SingleSearchBarWithCustom
              id="publisher"
              label="Publisher"
              value={formik.values.publisher}
              getOptionLabel={(option: { name: any; }) => option.name}
              onChange={onPublisherChange}
              fetchMethod={fetchPagedPublishers}
              styles={styles}
              error={formik.touched.publisher && Boolean(formik.errors.publisher)}
              helperText={formik.touched.publisher && formik.errors.publisher}
              onBlur={() => formik.setFieldTouched('publisher', true)}
            />

            <FormControl error={formik.touched.genre && Boolean(formik.errors.genre)}>
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
                {genreOptions}
              </Select>
              {formik.touched.genre && formik.errors.genre && (
                <FormHelperText>{formik.errors.genre}</FormHelperText>
              )}
            </FormControl>
          </div>
        </DialogContent>

        <DialogActions>
          <Button
            onClick={onClose}
            disabled={loading}
          >
            Cancel
          </Button>
          <Button
            type="submit"
            variant="contained"
            disabled={submitDisabled}
            loading={loading}
          >
            Add Book
          </Button>
        </DialogActions>
      </form>
    </Dialog>
  )
}