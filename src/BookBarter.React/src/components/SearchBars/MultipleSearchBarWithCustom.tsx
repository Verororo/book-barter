import { useState, useEffect, useCallback, useMemo } from 'react';
import {
  Autocomplete,
  TextField,
  CircularProgress,
  debounce,
  createFilterOptions
} from '@mui/material';
import { useNotification } from '../../contexts/Notification/UseNotification';

type BaseEntity = {
  id?: number;
}

interface MultipleSearchBarWithCustomProps<T extends BaseEntity> {
  id?: string;
  label?: string;
  value: T[];
  getOptionLabel: (option: T) => string;
  onChange: (event: React.SyntheticEvent, newValue: T[]) => void;
  fetchMethod: (query: string, idsToSkip?: number[]) => Promise<T[]>;
  AddDialog: React.ComponentType<{
    defaultName?: string;
    onClose: () => void;
    onEntityCreated: (entity: T) => void;
  }>;
  placeholder?: string;
  error?: boolean;
  helperText?: any;
  onBlur?: () => void;
  styles: CSSModuleClasses;
}

type CustomOption = {
  inputValue: string;
}

const MINIMUM_SEARCH_LENGTH = 3;
const DEBOUNCE_DELAY = 500;

function MultipleSearchBarWithCustom<T extends BaseEntity>({
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
}: MultipleSearchBarWithCustomProps<T>) {
  const [options, setOptions] = useState<T[]>([]);
  const [loading, setLoading] = useState(false);
  const [inputValue, setInputValue] = useState('');
  const [openAddDialog, setOpenAddDialog] = useState(false);
  const [dialogDefaultName, setDialogDefaultName] = useState<string | undefined>();

  const { showNotification } = useNotification();

  const filter = useMemo(
    () => createFilterOptions<T | CustomOption>(),
    []
  );

  const currentIds = useMemo(
    () => value.map(item => item.id!),
    [value]
  );

  const debouncedFetch = useMemo(
    () => debounce((query: string, idsToSkip: number[]) => {
      fetchMethod(query, idsToSkip)
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
    debouncedFetch(inputValue, currentIds);

    return () => {
      debouncedFetch.clear();
    };
  }, [inputValue, debouncedFetch, currentIds]);

  const handleAddEntityCreated = useCallback((newEntity: T) => {
    onChange(null as any, [...value, newEntity]);
    setInputValue('');
  }, [onChange, value]);

  const handleChange = useCallback(
    (event: React.SyntheticEvent, newValue: (T | CustomOption)[]) => {
      const lastSelection = newValue[newValue.length - 1];

      if (lastSelection && 'inputValue' in lastSelection) {
        setDialogDefaultName(lastSelection.inputValue);
        setOpenAddDialog(true);
        // Filter out the custom option
        const filteredValue = newValue.filter(
          (item): item is T => !('inputValue' in item)
        );
        onChange(event, filteredValue);
      } else {
        onChange(event, newValue as T[]);
      }
    },
    [onChange]
  );

  const handleInputChange = useCallback(
    (_event: React.SyntheticEvent, newInput: string) => {
      setInputValue(newInput);
    },
    []
  );

  const handleCloseDialog = useCallback(() => {
    setDialogDefaultName(undefined);
    setOpenAddDialog(false);
  }, []);

  const filterOptions = useCallback(
    (options: (T | CustomOption)[], params: any) => {
      const filtered = filter(options, params);
      const { inputValue } = params;

      if (
        inputValue.length >= MINIMUM_SEARCH_LENGTH &&
        !loading &&
        filtered.length === 0
      ) {
        filtered.push({ inputValue } as CustomOption);
      }

      return filtered;
    },
    [filter, loading]
  );

  const getOptionLabelWrapper = useCallback(
    (option: T | CustomOption | string): string => {
      if (typeof option === 'string') return option;
      if ('inputValue' in option) {
        return `Add "${option.inputValue}" to our database...`;
      }
      return getOptionLabel(option);
    },
    [getOptionLabel]
  );

  const isOptionEqualToValue = useCallback(
    (option: T | CustomOption, value: T | CustomOption) => {
      if ('inputValue' in option && 'inputValue' in value) {
        return option.inputValue === value.inputValue;
      }
      if ('id' in option && 'id' in value) {
        return option.id === value.id;
      }
      return false;
    },
    []
  );

  return (
    <>
      <Autocomplete
        id={id}
        className={styles.multipleSearchBar}
        multiple
        value={value}
        onBlur={onBlur}
        onChange={handleChange}
        inputValue={inputValue}
        onInputChange={handleInputChange}
        options={options}
        loading={loading}
        filterOptions={filterOptions}
        getOptionLabel={getOptionLabelWrapper}
        isOptionEqualToValue={isOptionEqualToValue}
        renderInput={(params) => (
          <TextField
            {...params}
            label={label}
            placeholder={value.length === 0 ? placeholder : 'Add more...'}
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
      {openAddDialog && (
        <AddDialog
          defaultName={dialogDefaultName}
          onClose={handleCloseDialog}
          onEntityCreated={handleAddEntityCreated}
        />
      )}
    </>
  );
}

export default MultipleSearchBarWithCustom;