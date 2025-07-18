import { useState, useEffect, useMemo, useCallback } from 'react';
import { Grid, TextField, MenuItem, Select, FormControl, InputLabel, FormHelperText } from '@mui/material';
import { useFormik } from 'formik';
import { createAuthorCommand, fetchPagedAuthors } from '../../../api/clients/author-client';
import { updateBook } from '../../../api/clients/book-client';
import { fetchPagedGenres } from '../../../api/clients/genre-client';
import { createPublisherCommand, fetchPagedPublishers } from '../../../api/clients/publisher-client';
import type {
  BookForModerationDto,
  UpdateBookCommand,
  AuthorDto,
  GenreDto,
  PublisherDto
} from '../../../api/generated';
import { AddCustomAuthor } from '../../BookItem/AddCustomAuthorDialog';
import MultipleSearchBarWithCustom from '../../SearchBars/MultipleSearchBarWithCustom';
import SingleSearchBarWithCustom from '../../SearchBars/SingleSearchBarWithCustom';
import EditActionButtons from '../Buttons/EditActionButtons';
import styles from './Form.module.css'
import { useNotification } from '../../../contexts/Notification/UseNotification';

interface BookFormProps {
  book: BookForModerationDto;
  onSave: () => void;
  onCancel: () => void;
}

type UpdateBookValues = {
  isbn: string;
  title: string;
  publicationDate: string;
  authors: AuthorDto[];
  genre: GenreDto | null;
  publisher: PublisherDto | null;
}

type UpdateBookErrors = {
  isbn?: string;
  title?: string;
  publicationDate?: string;
  authors?: string;
  genre?: string;
  publisher?: string;
}

const BookForm = ({ book, onSave, onCancel }: BookFormProps) => {
  const [loading, setLoading] = useState(false);
  const [genres, setGenres] = useState<GenreDto[]>([]);

  const { showNotification } = useNotification();

  useEffect(() => {
    fetchPagedGenres('')
      .then((response) => {
        setGenres(response);
      })
      .catch(_error => {
        showNotification("Failed to fetch genres from the server. Try again later.", "error");
      });
  }, []);

  const validate = useCallback((values: UpdateBookValues) => {
    const errors: UpdateBookErrors = {};

    if (!values.isbn) errors.isbn = 'ISBN is required.';
    else if (!/^\d+$/.test(values.isbn)) errors.isbn = 'ISBN must contain only digits.';
    else if (values.isbn.length !== 13) errors.isbn = 'ISBN length should be 13.';

    if (!values.title) errors.title = 'Title is required.';
    else if (values.title.length > 100) errors.title = 'Title cannot exceed 100 characters.';

    if (!values.publicationDate) errors.publicationDate = 'Publication date is required.';

    if (values.authors.length === 0) errors.authors = 'At least one author is required.';

    if (!values.genre?.id) errors.genre = 'Genre is required.';

    if (!values.publisher) errors.publisher = 'Publisher is required.';
    else if (values.publisher.name!.length > 30) errors.publisher = 'Publisher name cannot exceed 30 characters.'

    return errors;
  }, []);

  async function resolveAuthorsIds(authors: AuthorDto[]): Promise<AuthorDto[]> {
    return Promise.all(
      authors.map(async author => {
        if (author.id) return author;

        try {
          const newId = await createAuthorCommand({
            firstName: author.firstName,
            middleName: author.middleName,
            lastName: author.lastName!,
          });
          return { ...author, id: newId };
        } catch (error) {
          console.log(error)
          throw error;
        }
      })
    );
  }

  async function resolvePublisherId(publisher: PublisherDto): Promise<PublisherDto> {
    if (publisher.id) return publisher;

    try {
      const newId = await createPublisherCommand({
        name: publisher.name,
      });
      return { ...publisher, id: newId };
    } catch (error) {
      console.log(error)
      throw error;
    }
  }

  const formik = useFormik<UpdateBookValues>({
    initialValues: {
      isbn: book.isbn || '',
      title: book.title || '',
      publicationDate: book.publicationDate || '',
      authors: book.authors || [],
      genre: book.genre || null,
      publisher: book.publisher || null,
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

        const command: UpdateBookCommand = {
          id: book.id!,
          isbn: values.isbn,
          title: values.title,
          publicationDate: values.publicationDate,
          authorsIds: completeAuthors.map(a => a.id!),
          genreId: values.genre?.id!,
          publisherId: completePublisher?.id!
        };

        await updateBook(command);
        showNotification("Succesfully updated the book.", "success");
        onSave();
      }
      catch (error) {
        console.error(error);
        showNotification("Failed to update the book. Try again later.", "error");
      }
      finally {
        setLoading(false);
      }
    }
  });

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
    genres.map(genre => (
      <MenuItem key={genre.id} value={genre.id}>
        {genre.name}
      </MenuItem>
    )),
    [genres]
  );

  const displayedErrorFields = Object
    .keys(formik.errors)
    .filter(field => formik.touched[field as keyof UpdateBookValues]);

  const submitDisabled = displayedErrorFields.length > 0;

  return (
    <form onSubmit={formik.handleSubmit} className={styles.container}>
      <Grid container spacing={2}>
        <Grid size={{ xs: 12, sm: 6 }}>
          <TextField
            fullWidth
            label="Title"
            id="title"
            name="title"
            value={formik.values.title}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={Boolean(formik.errors.title)}
            helperText={formik.errors.title}
            variant="outlined"
            slotProps={{
              input: {
                className: styles.input
              }
            }}
          />
        </Grid>

        <Grid size={{ xs: 12, sm: 6 }}>
          <TextField
            fullWidth
            label="ISBN"
            id="isbn"
            name="isbn"
            value={formik.values.isbn}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={Boolean(formik.errors.isbn)}
            helperText={formik.errors.isbn}
            variant="outlined"
            slotProps={{
              input: {
                className: styles.input
              }
            }}
          />
        </Grid>

        <Grid size={{ xs: 12, sm: 6 }}>
          <TextField
            fullWidth
            label="Publication Date"
            name="publicationDate"
            id="publicationDate"
            type="date"
            value={formik.values.publicationDate}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={Boolean(formik.errors.publicationDate)}
            helperText={formik.errors.publicationDate}
            variant="outlined"
            slotProps={{
              input: {
                className: styles.input
              }
            }}
          />
        </Grid>

        <Grid size={{ xs: 12, sm: 6 }}>
          <FormControl
            fullWidth
            variant="outlined"
            error={Boolean(formik.errors.genre)}
          >
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
              name="genre"
              className={styles.input}
            >
              {genreOptions}
            </Select>
            {formik.errors.genre && (
              <FormHelperText>{formik.errors.genre}</FormHelperText>
            )}
          </FormControl>
        </Grid>

        <Grid size={{ xs: 12 }}>
          <SingleSearchBarWithCustom
            id="publisher"
            label="Publisher"
            value={formik.values.publisher}
            getOptionLabel={(option) => option.name!}
            onChange={onPublisherChange}
            fetchMethod={fetchPagedPublishers}
            createEntityFromName={(name) => ({ id: undefined, name: name })}
            styles={styles}
            error={Boolean(formik.errors.publisher)}
            helperText={formik.errors.publisher}
            onBlur={() => formik.setFieldTouched('publisher', true)}
          />
        </Grid>

        <Grid size={{ xs: 12 }}>
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
            error={Boolean(formik.errors.authors)}
            helperText={formik.errors.authors}
            onBlur={() => formik.setFieldTouched('authors', true)}
          />
        </Grid>
      </Grid>

      <EditActionButtons
        onCancel={onCancel}
        loading={loading}
        disabled={submitDisabled}
      />
    </form>
  );
};

export default BookForm;