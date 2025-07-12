import { useState, useEffect } from 'react';
import { Autocomplete, TextField, CircularProgress, debounce, createFilterOptions } from '@mui/material';

interface SingleSearchBarWithCustomProps {
  id?: string;
  label?: string;
  value: any;
  getOptionLabel: (option: any) => string;
  onChange: (_event: any, newValue: any) => void;
  fetchMethod: (query: any) => Promise<any>;
  AddDialog?: React.ComponentType<{
    defaultName?: string;
    onClose: () => void;
    onEntityCreated: (entity: any) => void;
  }>;
  placeholder?: string;
  error?: boolean;
  helperText?: any;
  onBlur?: () => void;
  styles: CSSModuleClasses;
}

type CustomOption = {
  inputValue?: string;
}

const SingleSearchBarWithCustom = ({
  id,
  label,
  value,
  getOptionLabel,
  onChange,
  fetchMethod,
  AddDialog,
  placeholder,
  error,
  helperText,
  onBlur,
  styles
}: SingleSearchBarWithCustomProps) => {
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

    setInputValue('');
  };

  return (
    <>
      <Autocomplete
        id={id}
        className={styles.singleSearchBar}
        value={value}
        onBlur={() => onBlur?.()}
        onChange={(event, newValue) => {
          if (typeof newValue === "string") {
            return
          }

          const isCustomOption = newValue?.inputValue;
          if (isCustomOption) {
            if (AddDialog) {
              setDialogDefaultName(newValue.inputValue)
              setOpenAddDialog(true);
            }
            else {
              onChange(event, { id: undefined, name: newValue.inputValue })
            }
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

          if (inputValue.length >= 3 && !loading) {
            filtered.push({ inputValue });
          }

          return filtered;
        }}
        getOptionLabel={option => {
          if (typeof option === 'string') return option;
          if (option.inputValue) return `Add "${inputValue}" to our database...`;

          return getOptionLabel(option);
        }}
        renderInput={(params) => (
          <TextField
            {...params}
            label={label}
            placeholder={value?.length === 0 ? placeholder : 'Add more...'}
            error={error}
            helperText={helperText}
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
      {(AddDialog && openAddDialog) && (
        <AddDialog
          defaultName={dialogDefaultName}
          onClose={() => {
            setDialogDefaultName(undefined)
            setOpenAddDialog(false)
          }}
          onEntityCreated={handleAddEntityCreated}
        />
      )
      }
    </>
  );
};

export default SingleSearchBarWithCustom;
