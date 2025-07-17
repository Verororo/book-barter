import { useEffect, useState } from 'react';
import UsersList from "../../components/UserItem/UsersList";
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner";

import { fetchListedUsersPaginatedResult } from "../../api/clients/user-client";
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
import type { AuthorDto, ListedBookDto } from '../../api/generated';
import MultipleSearchBar from '../../components/SearchBars/MultipleSearchBar';
import { fetchAutocompleteBooksSkipCustomIds } from '../../api/clients/book-client';

import styles from './Home.module.css'

const Home = () => {
  const { isAuthenticated } = useAuth();

  const ITEMS_PER_PAGE = 10;
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
    const fetchData = async () => {
      setLoading(true);
      try {
        const { items, total } = await fetchListedUsersPaginatedResult({
          pageNumber: currentPage,
          pageSize: ITEMS_PER_PAGE,
          orderByProperty: searchParams.sortBy,
          orderDirection: searchParams.sortDirection,
          wantedBooksIds: searchParams.booksGivenOut.map(b => b.id!) || undefined,
          ownedBooksIds: searchParams.booksLookedFor.map(b => b.id!) || undefined,
        });
        // Color the found books looked for in the search blue
        items.map(user => user.ownedBooks.map(b =>
          booksLookedFor.map(blf => blf.id).includes(b.id)
            ? b.color = "blue"
            : b.color = undefined))

        // Color the found books given out in the search orange
        items.map(user => user.wantedBooks.map(book =>
          booksGivenOut.map(blf => blf.id).includes(book.id)
            ? book.color = "orange"
            : book.color = undefined))

        setUsers(items);
        setTotal(total);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
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
    <>
      <header className={styles.logo}>
        <img src="/public/Logo.svg" alt="BookBarter logo" />
      </header>

      <div className={styles.userItemContainer}>
        <div className={styles.search}>
          <div className={styles.searchBarsContainer}>
            <div className={styles.multipleSearchBarRow}>
              <span className={styles.multipleSearchBarLabel}>I'm looking for</span>
              <MultipleSearchBar<ListedBookDto>
                value={booksLookedFor}
                onChange={(_event, v) => setBooksLookedFor(v)}
                fetchMethod={fetchAutocompleteBooksSkipCustomIds}
                placeholder="Enter the title of a book you'd like to get..."
                styles={styles}
                getOptionLabel={book => {
                  const authorName = book.authors!.length == 1
                    ? `${book.authors![0].firstName} ${book.authors![0].lastName}`.trim()
                    : book.authors?.map(((a: AuthorDto) => a.lastName)).join(", ")
                  return `${authorName}. ${book.title}`;
                }}
              />
            </div>

            <div className={styles.multipleSearchBarRow}>
              <span className={styles.multipleSearchBarLabel}>I'm giving out</span>
              <MultipleSearchBar<ListedBookDto>
                value={booksGivenOut}
                onChange={(_event, v) => setBooksGivenOut(v)}
                fetchMethod={fetchAutocompleteBooksSkipCustomIds}
                placeholder="Enter the title of a book you're ready to swap out..."
                styles={styles}
                getOptionLabel={book => {
                  const authorName = book.authors!.length == 1
                    ? `${book.authors![0].firstName} ${book.authors![0].lastName}`.trim()
                    : book.authors?.map(((a: AuthorDto) => a.lastName)).join(", ")
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
          <UsersList users={users} />
        )}

        <Pagination
          count={Math.ceil(total / ITEMS_PER_PAGE)}
          page={currentPage}
          onChange={handlePageChange}
          color="primary"
        />
      </div>
    </>
  )
}

export default Home