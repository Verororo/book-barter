import { useState, useEffect, useCallback, useMemo } from 'react';
import { Autocomplete, TextField, CircularProgress, debounce } from '@mui/material';

type BaseEntity = {
  id?: number;
}

interface MultipleSearchBarProps<T extends BaseEntity> {
  id?: string;
  label?: string;
  value: T[];
  getOptionLabel: (option: T) => string;
  onChange: (event: React.SyntheticEvent, newValue: T[]) => void;
  fetchMethod: (query: string, idsToSkip?: number[]) => Promise<T[]>;
  placeholder?: string;
  error?: boolean;
  helperText?: React.ReactNode;
  onBlur?: () => void;
  styles: CSSModuleClasses;
}

const MINIMUM_SEARCH_LENGTH = 3;
const DEBOUNCE_DELAY = 500;

function MultipleSearchBar<T extends BaseEntity>({
  id,
  label,
  value,
  getOptionLabel,
  onChange,
  fetchMethod,
  placeholder,
  error,
  helperText,
  onBlur,
  styles
}: MultipleSearchBarProps<T>) {
  const [options, setOptions] = useState<T[]>([]);
  const [loading, setLoading] = useState(false);
  const [inputValue, setInputValue] = useState('');

  const currentIds = useMemo(
    () => value.map(item => item.id!),
    [value]
  );

  const debouncedFetch = useMemo(
    () => debounce((query: string, idsToSkip: number[]) => {
      fetchMethod(query, idsToSkip)
        .then(setOptions)
        .catch(error => {
          console.error('Error fetching options:', error);
          setOptions([]);
        })
        .finally(() => setLoading(false));
    }, DEBOUNCE_DELAY),
    [fetchMethod]
  );

  useEffect(() => {
    if (inputValue.length < MINIMUM_SEARCH_LENGTH) {
      setLoading(false);
      setOptions([]);
      return;
    }

    setLoading(true);
    debouncedFetch(inputValue, currentIds);

    return () => {
      debouncedFetch.clear();
    };
  }, [inputValue, debouncedFetch, currentIds]);

  const handleInputChange = useCallback(
    (_event: React.SyntheticEvent, newInput: string) => {
      setInputValue(newInput);
    },
    []
  );

  const isOptionEqualToValue = useCallback(
    (option: T, value: T) => option.id === value.id,
    []
  );

  return (
    <Autocomplete
      id={id}
      className={styles.multipleSearchBar}
      multiple
      value={value}
      onChange={onChange}
      onBlur={onBlur}
      inputValue={inputValue}
      onInputChange={handleInputChange}
      options={options}
      loading={loading}
      filterOptions={(x) => x} // Disable client-side filtering
      getOptionLabel={getOptionLabel}
      isOptionEqualToValue={isOptionEqualToValue}
      renderInput={(params) => (
        <TextField
          {...params}
          className={styles.autocompleteRoot}
          label={label}
          placeholder={value.length === 0 ? placeholder : 'Add more...'}
          error={error}
          helperText={helperText}
          slotProps={{
            input: {
              ...params.InputProps,
              classes: {
                input: styles.input,
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
    />
  );
}

export default MultipleSearchBar;