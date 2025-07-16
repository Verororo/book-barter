import { useState, useEffect } from 'react';
import { Autocomplete, TextField, CircularProgress, debounce } from '@mui/material';

interface SingleSearchBarProps {
  id?: string;
  label?: string;
  value: any;
  getOptionLabel: (option: any) => string;
  onChange: (_event: any, newValue: any) => void;
  fetchMethod: (query: any) => Promise<any>;
  placeholder?: string;
  variant?: 'standard' | 'outlined' | 'filled';
  error?: boolean;
  styles: CSSModuleClasses;
}

const SingleSearchBar = ({ 
  id,
  label,
  value,
  getOptionLabel,
  onChange,
  fetchMethod,
  placeholder,
  variant,
  styles,
  error
}: SingleSearchBarProps) => {
  const [options, setOptions] = useState<any>();
  const [loading, setLoading] = useState(false);
  const [inputValue, setInputValue] = useState('');

  useEffect(() => {
    if (inputValue.length < 3) {
      setLoading(false);
      setOptions([]);
      return;
    }

    setLoading(true);

    const handler = debounce((query : string) => {
      fetchMethod(query)
        .then(options => setOptions(options))
        .finally(() => setLoading(false));
    }, 500);

    handler(inputValue);

    return () => {
      handler.clear();
    };
  }, [inputValue]);

  return (
    <Autocomplete
      id={id}
      className={styles.singleSearchBar}
      value={value}
      onChange={onChange}
      onInputChange={(_event, newInput) => setInputValue(newInput)}
      options={options}
      loading={loading}
      filterOptions={x => x} // Disable client-side filtering
      getOptionLabel={getOptionLabel}
      renderInput={(params) => (
        <TextField
          {...params}
          className={styles.autocompleteRoot}
          label={label}
          placeholder={placeholder}
          variant={variant}
          error={error}
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
};

export default SingleSearchBar;
