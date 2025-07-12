import { useState, useRef, useEffect } from "react";
import AddIcon from "@mui/icons-material/Add";
import Autocomplete, { createFilterOptions } from "@mui/material/Autocomplete";
import TextField from "@mui/material/TextField";
import ClickAwayListener from "@mui/material/ClickAwayListener";
import styles from "./NewBookAutocomplete.module.css";
import { fetchAutocompleteBooksPaginatedSkipRelated } from "../../api/book-client";
import debounce from "@mui/utils/debounce";
import { CircularProgress, MenuItem, Menu } from "@mui/material";
import { useAuth } from "../../contexts/Auth/UseAuth";
import { addBookToOwned } from "../../api/user-client";
import type { ListedBookDto } from "../../api/generated";
import { AddCustomBook } from "./AddCustomBookDialog";

type ListedBookDtoOptions = ListedBookDto & {
  inputValue?: string;
}

type NewBookAutocompleteProps = {
  isGivingOut?: boolean;
  onBookAdded?: (book: ListedBookDto, bookStateName: string) => void;
}

const NewBookAutocomplete = ({ isGivingOut = false, onBookAdded }: NewBookAutocompleteProps) => {
  const { userAuthData } = useAuth();
  const userId = userAuthData!.id;

  const [openAutocomplete, setOpenAutocomplete] = useState(false);
  const inputRef = useRef<HTMLInputElement | null>(null);

  const [options, setOptions] = useState<ListedBookDtoOptions[]>([]);
  const [loading, setLoading] = useState(false);
  const [value, setValue] = useState<ListedBookDtoOptions | null>(null);
  const [inputValue, setInputValue] = useState('');

  const [anchorEl, setAnchorEl] = useState<HTMLElement | null>(null);
  const [selectedBook, setSelectedBook] = useState<ListedBookDtoOptions | null>(null);

  const [openAddDialog, setOpenAddDialog] = useState(false);
  const [dialogDefaultTitle, setDialogDefaultTitle] = useState<string>();

  const [customBookStateId, setCustomBookStateId] = useState<number | null>(null);

  const bookStates = [
    { id: 1, label: 'Old' },
    { id: 2, label: 'Medium' },
    { id: 3, label: 'New' },
  ];

  const filter = createFilterOptions<ListedBookDtoOptions>();

  useEffect(() => {
    if (openAutocomplete && inputRef.current) inputRef.current.focus();
  }, [openAutocomplete]);

  useEffect(() => {
    if (inputValue.length < 3) {
      setLoading(false);
      setOptions([]);
      return;
    }

    setLoading(true);
    const handler = debounce(title => {
      fetchAutocompleteBooksPaginatedSkipRelated(title)
        .then(options => setOptions(options))
        .finally(() => setLoading(false));
    }, 500);

    handler(inputValue);

    return () => { handler.clear(); }
  }, [inputValue]);

  const handleDialog = (bookStateId = 0) => {
    setCustomBookStateId(bookStateId)
    setAnchorEl(null);
    setOpenAddDialog(true)
  }

  const addBookRelationship = async (book: ListedBookDto, bookStateId: number) => {
    if (!book)
      return

    await addBookToOwned(
      {
        bookId: book.id,
        bookStateId
      }
    );

    setAnchorEl(null);
    setValue(null);
    setInputValue('');
    setOptions([]);

    const bookStateName = bookStates.find(bs => bs.id === bookStateId)!.label
    onBookAdded?.(book, bookStateName);
    setSelectedBook(null);
  }

  const handleCustomBookCreated = (listedBook: ListedBookDto) => {
    if (customBookStateId !== null) {
      addBookRelationship(listedBook, customBookStateId);
      setCustomBookStateId(null);
    }
  }

  return (
    <>
      <ClickAwayListener onClickAway={() => setOpenAutocomplete(false)}>
        <div
          className={`${styles.container} ${openAutocomplete ? styles.expanded : styles.collapsed}`}
          onClick={() => setOpenAutocomplete(true)}
        >
          {openAutocomplete ? (
            <>
              <Autocomplete
                freeSolo
                value={value}
                onChange={(_event, newValue) => {
                  if (typeof newValue === 'string') {
                    return
                  }

                  if (newValue && newValue.inputValue) {
                    setDialogDefaultTitle(newValue.inputValue);
                    if (isGivingOut) {
                      setAnchorEl(inputRef.current as HTMLElement)
                    }
                  }
                  else if (newValue) {
                    setValue(newValue);
                    setSelectedBook(newValue);
                    if (isGivingOut) {
                      setAnchorEl(inputRef.current as HTMLElement)
                    }
                  }
                }}
                onInputChange={(_event, newInput) => setInputValue(newInput)}
                loading={loading}
                filterOptions={(options, params) => {
                  const filtered = filter(options, params);
                  const { inputValue } = params;

                  if (inputValue.length >= 3 && !loading) {
                    filtered.push({ inputValue });
                  }
                  return filtered;
                }}
                options={options}
                getOptionLabel={option => {
                  if (typeof option === 'string') return option;
                  if (option.inputValue) return `Add "${inputValue}" to our database...`;

                  const authors = option.authors!.length === 1
                    ? `${option.authors![0].firstName} ${option.authors![0].lastName}`.trim()
                    : option.authors?.map(a => a.lastName).join(', ');
                  return `${authors}. ${option.title}`;
                }}
                renderInput={params => (
                  <TextField
                    {...params}
                    inputRef={node => { if (node) inputRef.current = node; }}
                    placeholder="Enter the title of a book…"
                    slotProps={{
                      input: {
                        ...params.InputProps,
                        classes: {
                          input: styles.textInput,
                        },
                        endAdornment: (
                          <>
                            {loading && <CircularProgress size={20} />}
                            {params.InputProps.endAdornment}
                          </>
                        ),
                      },
                    }}
                  />
                )}
                onClose={() => {
                  if (!openAddDialog) {
                    setOpenAutocomplete(false)
                  }
                }}
              />

              <Menu
                anchorEl={anchorEl}
                open={Boolean(anchorEl)}
                onClose={() => setAnchorEl(null)}
              >
                {bookStates.map(state => (
                  <MenuItem key={state.id} onClick={() => {
                    if (dialogDefaultTitle) {
                      handleDialog(state.id)
                    }
                    else if (selectedBook) {
                      addBookRelationship(selectedBook, state.id)
                    }
                  }}>
                    {state.label}
                  </MenuItem>
                ))}
              </Menu>
            </>
          ) : (
            <AddIcon />
          )}
        </div>
      </ClickAwayListener>

      {openAddDialog && (
        <AddCustomBook
          defaultTitle={dialogDefaultTitle}
          onClose={() => {
            setDialogDefaultTitle(undefined)
            setOpenAddDialog(false)
          }}
          onBookCreated={handleCustomBookCreated}
        />
      )}
    </>
  );
};

export default NewBookAutocomplete;
