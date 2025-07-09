import UserItem from "./UserItem";
import styles from './UserItemContainer.module.css';
import LoadingSpinner from "../LoadingSpinner/LoadingSpinner";

import { useEffect, useState } from "react";
import { fetchListedUsersPaginated } from "../../api/user-client";
import type { ListedUser } from "../../api/view-models/listed-user";

import {
  Pagination,
  TextField,
  RadioGroup,
  FormControlLabel,
  Radio,
  Button,
  Collapse,
  Autocomplete,
  CircularProgress,
  debounce
} from "@mui/material";
import { fetchAutocompleteBooksPaginated } from "../../api/book-client";
import type { AutocompleteBookItem } from "../../api/view-models/autocomplete-book";
import { useAuth } from "../../contexts/Auth/UseAuth";

const UserItemContainer = () => {
  const { isAuthenticated, userAuthData } = useAuth()

  const pageSize = 10;
  const [users, setUsers] = useState<ListedUser[]>([]);
  const [total, setTotal] = useState(0);
  const [currentPage, setCurrentPage] = useState(1);
  const [loading, setLoading] = useState<boolean>(false);

  // Filtering and ordering states
  const [booksLookedFor, setBooksLookedFor] = useState<AutocompleteBookItem[]>([]);
  const [booksGivenOut, setBooksGivenOut] = useState<AutocompleteBookItem[]>([]);
  const [sortBy, setSortBy] = useState<string>("lastOnlineDate");
  const [sortDirection, setSortDirection] = useState<string>("desc");
  const [showAdvanced, setShowAdvanced] = useState<boolean>(false);

  // Autocomplete states
  const [bookOptions, setBookOptions] = useState<AutocompleteBookItem[]>([]);
  const [isLookingForOptionsLoading, setLookingForOptionsLoading] = useState(false);
  const [isGivingOutOptionsLoading, setGivingOutOptionsLoading] = useState(false);
  const [lookingForAutocompleteInput, setLookingForAutocompleteInput] = useState("");
  const [givingOutAutocompleteInput, setGivingOutAutocompleteInput] = useState("");

  // State to hold the committed search params for the API call
  const [searchParams, setSearchParams] = useState({
    booksLookedFor,
    booksGivenOut,
    sortBy,
    sortDirection
  });

  // UseEffect for the wanted books searchbar
  useEffect(() => {
    if (lookingForAutocompleteInput.length < 3) {
      setLookingForOptionsLoading(false)
      setBookOptions([])
      return
    }

    setLookingForOptionsLoading(true);

    // Send a request only after 0.5s of input absence
    const handler = debounce((input : string) => {
      fetchAutocompleteBooksPaginated(input)
        .then(formattedOptions => {
          setBookOptions(formattedOptions)
        })
        .finally(() => {
          setLookingForOptionsLoading(false)
        })
    }, 500)

    handler(lookingForAutocompleteInput)

    return () => {
      handler.clear()
    }
  }, [lookingForAutocompleteInput]);

  // UseEffect for the owned books searchbar
  useEffect(() => {
    if (givingOutAutocompleteInput.length < 3) {
      setGivingOutOptionsLoading(false);
      setBookOptions([]);
      return;
    }

    setGivingOutOptionsLoading(true);

    // Send a request only after 0.5s of input absence
    const handler = debounce((input: string) => {
      fetchAutocompleteBooksPaginated(input)
        .then(formattedOptions => {
          setBookOptions(formattedOptions);
        })
        .finally(() => {
          setGivingOutOptionsLoading(false);
        });
    }, 500);

    handler(givingOutAutocompleteInput)

    return () => {
      handler.clear()
    }
  }, [givingOutAutocompleteInput]);

  useEffect(() => {
    setLoading(true);
    fetchListedUsersPaginated({
      pageNumber: currentPage,
      pageSize,
      orderByProperty: searchParams.sortBy,
      orderDirection: searchParams.sortDirection,
      wantedBooksIds: booksGivenOut.length > 0 ? booksGivenOut.map(ob => ob.id) : undefined,
      ownedBooksIds: booksLookedFor.length > 0 ? booksLookedFor.map(wb => wb.id) : undefined,
      userToSkipId: isAuthenticated ? userAuthData?.id : undefined
    })
      .then(({ items, total }) => {
        setUsers(items);
        setTotal(total);
      })
      .catch(console.error)
      .finally(() => {
        setLoading(false);
      });
  }, [currentPage, searchParams]);

  const handleSearch = () => {
    setCurrentPage(1); // Reset to first page on new search
    setSearchParams({
      booksLookedFor,
      booksGivenOut,
      sortBy,
      sortDirection
    });
  };

  const scrollToTop = () =>
    window.scrollTo({ top: 600, behavior: 'smooth' });

  const handlePageChange = (_: any, page: number) => {
    setCurrentPage(page);
    setTimeout(scrollToTop, 50);
  };

  return (
    <div className={styles.userItemContainer}>
      <div className={styles.search}>
        <div className={styles.searchBarsContainer}>
          <div className={styles.searchBarRow}>
            <span className={styles.searchBarLabel}>I'm looking for</span>
            <Autocomplete
              className={styles.searchBar}
              multiple
              value={booksLookedFor}
              onChange={(_event, newValue) => {
                setBookOptions([]); // Clear options after selection
                setBooksLookedFor(newValue);
              }}
              onInputChange={(_event, newInputValue) => {
                setLookingForAutocompleteInput(newInputValue);
              }}
              options={bookOptions}
              loading={isLookingForOptionsLoading}
              filterOptions={x => x} // Disable client-side filtering
              getOptionLabel={option => option.info}
              renderInput={params => (
                <TextField
                  {...params}
                  placeholder={booksLookedFor.length === 0
                    ? "Enter the title of a book you'd like to get..." 
                    : "Add more..."}
                  slotProps={{
                    input: {
                      ...params.InputProps,
                      endAdornment: (
                        <>
                          {isLookingForOptionsLoading && (
                            <CircularProgress size={20} />
                          )}
                          {params.InputProps.endAdornment}
                        </>
                      ),
                    },
                  }}
                />
              )}
            />
          </div>

          <div className={styles.searchBarRow}>
            <span className={styles.searchBarLabel}>I'm giving out</span>
            <Autocomplete
              className={styles.searchBar}
              multiple
              value={booksGivenOut}
              onChange={(_event, newValue) => {
                setBookOptions([]); // Clear options after selection
                setBooksGivenOut(newValue);
              }}
              onInputChange={(_event, newInputValue) => {
                setGivingOutAutocompleteInput(newInputValue);
              }}
              options={bookOptions}
              loading={isGivingOutOptionsLoading}
              filterOptions={x => x} // Disable client-side filtering
              getOptionLabel={option => option.info}
              renderInput={params => (
                <TextField
                  {...params}
                  placeholder={booksGivenOut.length === 0
                    ? "Enter the title of a book you're ready to swap out..."
                    : "Add more..."}
                  slotProps={{
                    input: {
                      ...params.InputProps,
                      endAdornment: (
                        <>
                          {isGivingOutOptionsLoading && (
                            <CircularProgress color="inherit" size={20} />
                          )}
                          {params.InputProps.endAdornment}
                        </>
                      ),
                    },
                  }}
                />
              )}
            />
          </div>
        </div>

        <div className={styles.controlButtons}>
          <Button variant="text" onClick={() => setShowAdvanced(!showAdvanced)}>
            Advanced
          </Button>
          <Button variant="contained" color="primary" onClick={handleSearch}>
            Search!
          </Button>
        </div>

        <Collapse in={showAdvanced}>
          <div className={styles.advancedSettingsContainer}>
            <div className={styles.orderingSettings}>
              <span>Sort By:</span>
              <RadioGroup row value={sortBy} onChange={(e) => setSortBy(e.target.value)}>
                <FormControlLabel value="lastOnlineDate" control={<Radio />} label="Last Online" />
                <FormControlLabel value="registrationDate" control={<Radio />} label="Registration Date" />
                <FormControlLabel value="userName" control={<Radio />} label="User Name" />
              </RadioGroup>
            </div>

            <div className={styles.orderingSettings}>
              <span>Sort Direction:</span>
              <RadioGroup row value={sortDirection} onChange={(e) => setSortDirection(e.target.value)}>
                <FormControlLabel value="asc" control={<Radio />} label="Ascending" />
                <FormControlLabel value="desc" control={<Radio />} label="Descending" />
              </RadioGroup>
            </div>
          </div>
        </Collapse>
      </div>

      {loading
        ? <LoadingSpinner />
        : users.map(user => (
          <UserItem key={user.userName} user={user} />
        ))}

      <Pagination
        count={Math.ceil(total / pageSize)}
        page={currentPage}
        onChange={handlePageChange}
        color="primary"
      />
    </div>
  );
};

export default UserItemContainer;