import { useState, useEffect } from 'react';
import { Autocomplete, TextField, CircularProgress, debounce } from '@mui/material';

interface MultipleSearchBarProps {
  id?: string;
  label?: string;
  value: any;
  getOptionLabel: (option: any) => string;
  onChange: (_event: any, newValue: any) => void;
  fetchMethod: (query: any) => Promise<any>;
  placeholder?: string;
  styles: CSSModuleClasses;
}

const MultipleSearchBar = ({
  id,
  label,
  value,
  getOptionLabel,
  onChange,
  fetchMethod,
  placeholder,
  styles
}: MultipleSearchBarProps) => {
  const [options, setOptions] = useState<any>([]);
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
      className={styles.multipleSearchBar}
      multiple
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
          label={label}
          placeholder={value.length === 0 ? placeholder : 'Add more...'}
          slotProps={{
            input: {
              ...params.InputProps,
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

export default MultipleSearchBar;
