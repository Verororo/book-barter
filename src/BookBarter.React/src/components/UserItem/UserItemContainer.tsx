import { useEffect, useState } from 'react';
import UserItem from "../UserItem/UserItem";
import LoadingSpinner from "../LoadingSpinner/LoadingSpinner";
import styles from './UserItemContainer.module.css';

import { fetchListedUsersPaginated } from "../../api/clients/user-client";
import type { ListedUser } from "../../api/view-models/listed-user";

import {
  Pagination,
  RadioGroup,
  FormControlLabel,
  Radio,
  Button,
  Collapse,
} from "@mui/material";
import { useAuth } from "../../contexts/Auth/UseAuth";
import type { ListedBookDto } from '../../api/generated';
import MultipleSearchBar from '../SearchBars/MultipleSearchBar';
import { fetchAutocompleteBooksPaginated } from '../../api/clients/book-client';

const UserItemContainer = () => {
  const { isAuthenticated } = useAuth();

  const pageSize = 10;
  const [users, setUsers] = useState<ListedUser[]>([]);
  const [total, setTotal] = useState(0);
  const [currentPage, setCurrentPage] = useState(1);
  const [loading, setLoading] = useState(false);

  const [booksLookedFor, setBooksLookedFor] = useState<ListedBookDto[]>([]);
  const [booksGivenOut, setBooksGivenOut] = useState<ListedBookDto[]>([]);

  const [sortBy, setSortBy] = useState("lastOnlineDate");
  const [sortDirection, setSortDirection] = useState("desc");
  const [showAdvanced, setShowAdvanced] = useState(false);

  const [searchParams, setSearchParams] = useState({
    booksLookedFor,
    booksGivenOut,
    sortBy,
    sortDirection,
  });

  useEffect(() => {
    setLoading(true);
    fetchListedUsersPaginated({
      pageNumber: currentPage,
      pageSize,
      orderByProperty: searchParams.sortBy,
      orderDirection: searchParams.sortDirection,
      wantedBooksIds: searchParams.booksGivenOut.map(b => b.id!) || undefined,
      ownedBooksIds: searchParams.booksLookedFor.map(b => b.id!) || undefined,
    })
      .then(({ items, total }) => {
        setUsers(items);
        setTotal(total);
      })
      .catch(console.error)
      .finally(() => setLoading(false));
  }, [currentPage, searchParams, isAuthenticated]);

  const handleSearch = () => {
    setCurrentPage(1);
    setSearchParams({ booksLookedFor, booksGivenOut, sortBy, sortDirection });
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
          <div className={styles.multipleSearchBarRow}>
            <span className={styles.multipleSearchBarLabel}>I'm looking for</span>
            <MultipleSearchBar
              value={booksLookedFor}
              onChange={(_event, v) => setBooksLookedFor(v)}
              fetchMethod={fetchAutocompleteBooksPaginated}
              placeholder="Enter the title of a book you'd like to get..."
              styles={styles}
              getOptionLabel={book => {
                const authorName = book.authors!.length == 1
                  ? `${book.authors![0].firstName} ${book.authors![0].lastName}`.trim()
                  : book.authors?.map((a: { lastName: any; }) => a.lastName).join(", ")
                return `${authorName}. ${book.title}`;
              }}
            />
          </div>

          <div className={styles.multipleSearchBarRow}>
            <span className={styles.multipleSearchBarLabel}>I'm giving out</span>
            <MultipleSearchBar
              value={booksGivenOut}
              onChange={(_event, v) => setBooksGivenOut(v)}
              fetchMethod={fetchAutocompleteBooksPaginated}
              placeholder="Enter the title of a book you're ready to swap out..."
              styles={styles}
              getOptionLabel={book => {
                const authorName = book.authors!.length == 1
                  ? `${book.authors![0].firstName} ${book.authors![0].lastName}`.trim()
                  : book.authors?.map((a: { lastName: any; }) => a.lastName).join(", ")
                return `${authorName}. ${book.title}`;
              }}
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
              <RadioGroup row value={sortBy} onChange={e => setSortBy(e.target.value)}>
                <FormControlLabel value="lastOnlineDate" control={<Radio />} label="Last Online" />
                <FormControlLabel value="registrationDate" control={<Radio />} label="Registration Date" />
                <FormControlLabel value="userName" control={<Radio />} label="User Name" />
              </RadioGroup>
            </div>
            <div className={styles.orderingSettings}>
              <span>Sort Direction:</span>
              <RadioGroup row value={sortDirection} onChange={e => setSortDirection(e.target.value)}>
                <FormControlLabel value="asc" control={<Radio />} label="Ascending" />
                <FormControlLabel value="desc" control={<Radio />} label="Descending" />
              </RadioGroup>
            </div>
          </div>
        </Collapse>
      </div>

      {loading ? (
        <LoadingSpinner />
      ) : (
        users.map(user => <UserItem key={user.userName} user={user} />)
      )}

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