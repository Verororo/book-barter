import { useState, useEffect, useCallback, useMemo } from 'react';
import { Autocomplete, TextField, CircularProgress, debounce } from '@mui/material';
import { useNotification } from '../../contexts/Notification/UseNotification';

type BaseEntity = {
  id?: number;
}

interface SingleSearchBarProps<T extends BaseEntity> {
  id?: string;
  label?: string;
  value: T | null;
  getOptionLabel: (option: T) => string;
  onChange: (event: React.SyntheticEvent, newValue: T | null) => void;
  fetchMethod: (query: string) => Promise<T[]>;
  placeholder?: string;
  error?: boolean;
  helperText?: React.ReactNode;
  onBlur?: () => void;
  styles: CSSModuleClasses;
  variant?: 'standard' | 'outlined' | 'filled';
}

const MINIMUM_SEARCH_LENGTH = 3;
const DEBOUNCE_DELAY = 500;

function SingleSearchBar<T extends BaseEntity>({
  id,
  label,
  value,
  getOptionLabel,
  onChange,
  fetchMethod,
  placeholder,
  variant = 'outlined',
  error,
  helperText,
  onBlur,
  styles
}: SingleSearchBarProps<T>) {
  const [options, setOptions] = useState<T[]>([]);
  const [loading, setLoading] = useState(false);
  const [inputValue, setInputValue] = useState('');

  const { showNotification } = useNotification();

  const debouncedFetch = useMemo(
    () => debounce((query: string) => {
      fetchMethod(query)
        .then(setOptions)
        .catch(_error => {
          showNotification("Failed to fetch autocomplete options. Try again later.", "error")
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
    debouncedFetch(inputValue);

    return () => {
      debouncedFetch.clear();
    };
  }, [inputValue, debouncedFetch]);

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
      className={styles.singleSearchBar}
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
          label={label}
          placeholder={placeholder}
          variant={variant}
          error={error}
          helperText={helperText}
          slotProps={{
            input: {
              ...params.InputProps,
              className: styles.input,
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

export default SingleSearchBar;