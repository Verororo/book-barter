import { useState, useEffect } from 'react';
import { Autocomplete, TextField, CircularProgress, debounce, createFilterOptions, Chip } from '@mui/material';
import { AddCustomAuthor } from '../BookItem/AddCustomAuthorDialog';

interface AuthorsSearchBarProps {
  id?: string;
  label?: string;
  value: any;
  getOptionLabel: (option: any) => string;
  onChange: (_event: any, newValue: any) => void;
  fetchMethod: (query: any) => Promise<any>;
  placeholder?: string;
  styles: CSSModuleClasses;
}

type CustomOption = {
  inputValue?: string;
}

const AuthorsSearchBar = ({
  id,
  label,
  value,
  getOptionLabel,
  onChange,
  fetchMethod,
  placeholder,
  styles
}: AuthorsSearchBarProps) => {
  const [options, setOptions] = useState<any>([]);
  const [loading, setLoading] = useState(false);
  const [inputValue, setInputValue] = useState('');

  const [openAddDialog, setOpenAddDialog] = useState(false)

  const [dialogDefaultName, setDialogDefaultName] = useState<string>()

  const filter = createFilterOptions<any & CustomOption>()

  useEffect(() => {
    if (inputValue.length < 3) {
      setLoading(false);
      setOptions([]);
      return;
    }

    setLoading(true);

    const handler = debounce((query: string) => {
      fetchMethod(query)
        .then(options => setOptions(options))
        .finally(() => setLoading(false));
    }, 500);

    handler(inputValue);

    return () => {
      handler.clear();
    };
  }, [inputValue]);

  const handleAddEntityCreated = (newEntity: any) => {
    onChange(null, [...value, newEntity]);

    setOptions((prev: any) => [...prev, newEntity]);
    setInputValue('');
  };

  return (
    <>
      <Autocomplete
        id={id}
        className={styles.multipleSearchBar}
        multiple
        value={value}
        onChange={(event, newValue) => {
          const lastSelection = newValue[newValue.length - 1];
          const isCustomOption = lastSelection?.inputValue;

          if (isCustomOption) {
            setDialogDefaultName(lastSelection.inputValue)

            setOpenAddDialog(true);

            onChange(event, newValue.filter(item => !item.inputValue));
          }
          else {
            onChange(event, newValue);
          }
        }}
        inputValue={inputValue}
        onInputChange={(_event, newInput) => setInputValue(newInput)}
        options={options}
        loading={loading}
        filterOptions={(options, params) => {
          const filtered = filter(options, params);
          const { inputValue } = params;

          if (inputValue.trim() !== '' && !loading) {
            filtered.push({ inputValue });
          }

          return filtered;
        }}
        getOptionLabel={option => {
          if (typeof option === 'string') return option;
          if (option.inputValue) return `Add "${inputValue}" to our database...`;

          return getOptionLabel(option);
        }}
        renderValue={(value, getTagProps) =>
          value.map((option, index) => (
            <Chip
              {...getTagProps({ index })}
              key={option.id || index}
              label={getOptionLabel(option)}
            />
          ))
        }
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
      {openAddDialog && (
        <AddCustomAuthor
          defaultName={dialogDefaultName}
          onClose={() => {
            setDialogDefaultName(undefined)
            setOpenAddDialog(false)
          }}
          onAuthorCreated={handleAddEntityCreated}
        />
      )
      }
    </>
  );
};

export default AuthorsSearchBar;
