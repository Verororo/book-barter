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
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  type SelectChangeEvent,
  Grid,
  TextField
} from "@mui/material";
import { useAuth } from "../../contexts/Auth/UseAuth";
import { type AuthorDto, type ListedBookDto, type GenreDto, type PublisherDto, type CityDto } from '../../api/generated';
import MultipleSearchBar from '../../components/SearchBars/MultipleSearchBar';
import SingleSearchBar from '../../components/SearchBars/SingleSearchBar';
import { fetchAutocompleteBooksSkipCustomIds } from '../../api/clients/book-client';
import { fetchPagedAuthors } from '../../api/clients/author-client';
import { fetchPagedPublishers } from '../../api/clients/publisher-client';
import { fetchPagedGenres } from '../../api/clients/genre-client';

import styles from './Home.module.css'
import { useNotification } from '../../contexts/Notification/UseNotification';
import { fetchPagedCities } from '../../api/clients/city-client';

const Home = () => {
  const { isAuthenticated } = useAuth();

  const ITEMS_PER_PAGE = 10;
  const [users, setUsers] = useState<ListedUser[]>([]);
  const [total, setTotal] = useState(0);
  const [currentPage, setCurrentPage] = useState(1);
  const [loading, setLoading] = useState(false);

  const [booksLookedFor, setBooksLookedFor] = useState<ListedBookDto[]>([]);
  const [booksGivenOut, setBooksGivenOut] = useState<ListedBookDto[]>([]);

  // Advanced search states for users
  const [userName, setUserName] = useState<string | null>(null)
  const [userCity, setUserCity] = useState<CityDto | null>(null)

  // Advanced search states for owned books
  const [ownedBookAuthors, setOwnedBookAuthors] = useState<AuthorDto[]>([]);
  const [ownedBookGenre, setOwnedBookGenre] = useState<GenreDto | null>(null);
  const [ownedBookPublisher, setOwnedBookPublisher] = useState<PublisherDto | null>(null);

  // Advanced search states for wanted books
  const [wantedBookAuthors, setWantedBookAuthors] = useState<AuthorDto[]>([]);
  const [wantedBookGenre, setWantedBookGenre] = useState<GenreDto | null>(null);
  const [wantedBookPublisher, setWantedBookPublisher] = useState<PublisherDto | null>(null);

  const [genres, setGenres] = useState<GenreDto[]>([]);

  const [sortBy, setSortBy] = useState("lastOnlineDate");
  const [sortDirection, setSortDirection] = useState("desc");
  const [showAdvanced, setShowAdvanced] = useState(false);

  const [searchParams, setSearchParams] = useState({
    userName,
    userCity,
    booksLookedFor,
    booksGivenOut,
    ownedBookAuthors,
    ownedBookGenre,
    ownedBookPublisher,
    wantedBookAuthors,
    wantedBookGenre,
    wantedBookPublisher,
    sortBy,
    sortDirection,
  });

  const { showNotification } = useNotification();

  useEffect(() => {
    fetchPagedGenres('')
      .then((response) => {
        setGenres(response);
      })
      .catch(_error => {
        showNotification("Failed to fetch genres from the server. Try again later.", "error");
      });
  }, []);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const { items, total } = await fetchListedUsersPaginatedResult({
          pageNumber: currentPage,
          pageSize: ITEMS_PER_PAGE,
          userName: userName || undefined,
          cityId: userCity?.id || undefined,
          orderByProperty: searchParams.sortBy,
          orderDirection: searchParams.sortDirection,
          wantedBooksIds: searchParams.booksGivenOut.map(b => b.id!) || undefined,
          ownedBooksIds: searchParams.booksLookedFor.map(b => b.id!) || undefined,
          ownedBookAuthorsIds: searchParams.ownedBookAuthors.length > 0
            ? searchParams.ownedBookAuthors.map(a => a.id!)
            : undefined,
          ownedBookGenreId: searchParams.ownedBookGenre?.id || undefined,
          ownedBookPublisherId: searchParams.ownedBookPublisher?.id || undefined,
          wantedBookAuthorsIds: searchParams.wantedBookAuthors.length > 0
            ? searchParams.wantedBookAuthors.map(a => a.id!)
            : undefined,
          wantedBookGenreId: searchParams.wantedBookGenre?.id || undefined,
          wantedBookPublisherId: searchParams.wantedBookPublisher?.id || undefined,
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
        showNotification("Failed to fetch users. Try again later.", "error")
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [currentPage, searchParams, isAuthenticated]);

  const handleSearch = () => {
    setCurrentPage(1);
    setSearchParams({
      userName,
      userCity,
      booksLookedFor,
      booksGivenOut,
      ownedBookAuthors,
      ownedBookGenre,
      ownedBookPublisher,
      wantedBookAuthors,
      wantedBookGenre,
      wantedBookPublisher,
      sortBy,
      sortDirection
    });
  };

  const handleClearSearch = () => {
    setUserName(null);
    setUserCity(null);
    setBooksLookedFor([]);
    setBooksGivenOut([]);
    setOwnedBookAuthors([]);
    setOwnedBookGenre(null);
    setOwnedBookPublisher(null);
    setWantedBookAuthors([]);
    setWantedBookGenre(null);
    setWantedBookPublisher(null);
    setSortBy("lastOnlineDate");
    setSortDirection("desc");
    setCurrentPage(1);
    setSearchParams({
      userName: null,
      userCity: null,
      booksLookedFor: [],
      booksGivenOut: [],
      ownedBookAuthors: [],
      ownedBookGenre: null,
      ownedBookPublisher: null,
      wantedBookAuthors: [],
      wantedBookGenre: null,
      wantedBookPublisher: null,
      sortBy: "lastOnlineDate",
      sortDirection: "desc",
    });
  };

  const scrollToTop = () =>
    window.scrollTo({ top: 600, behavior: 'smooth' });

  const handlePageChange = (_: any, page: number) => {
    setCurrentPage(page);
    setTimeout(scrollToTop, 50);
  };

  const handleOwnedGenreChange = (event: SelectChangeEvent<number>) => {
    const genreId = event.target.value as number;
    const selectedGenre = genres.find(g => g.id === genreId) || null;
    setOwnedBookGenre(selectedGenre);
  };

  const handleWantedGenreChange = (event: SelectChangeEvent<number>) => {
    const genreId = event.target.value as number;
    const selectedGenre = genres.find(g => g.id === genreId) || null;
    setWantedBookGenre(selectedGenre);
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
              <div className={styles.topSearchBar}>
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
            </div>

            <div className={styles.multipleSearchBarRow}>
              <span className={styles.multipleSearchBarLabel}>I'm giving out</span>
              <div className={styles.topSearchBar}>
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
          </div>

          <div className={styles.controlButtons}>
            <Button variant="text" onClick={() => setShowAdvanced(!showAdvanced)}>
              Advanced
            </Button>
            <Button variant="contained" color="primary" onClick={handleSearch}>
              Search!
            </Button>
            <Button variant="outlined" onClick={handleClearSearch}>
              Clear
            </Button>
          </div>

          <Collapse in={showAdvanced}>
            <div className={styles.advancedSettingsContainer}>
              <div className={styles.advancedSearchSection}>
                <h3 className={styles.advancedSearchTitle}>User Properties</h3>
                <Grid container spacing={2}>
                  <Grid size={{ xs: 12, sm: 6 }}>
                    <TextField
                      className={styles.input}
                      label="User Name"
                      value={userName}
                      onChange={(e: React.ChangeEvent<HTMLInputElement>) => setUserName(e.target.value)}
                      variant="outlined"
                      fullWidth
                    />
                  </Grid>
                  <Grid size={{ xs: 12, sm: 6 }}>
                    <SingleSearchBar<CityDto>
                      label="User City"
                      value={userCity}
                      onChange={(_event, v) => setUserCity(v)}
                      fetchMethod={fetchPagedCities}
                      placeholder="Search for city..."
                      styles={styles}
                      getOptionLabel={(city) => city?.nameWithCountry || ''}
                    />
                  </Grid>
                </Grid>
              </div>
              <div className={styles.advancedSearchSection}>
                <h3 className={styles.advancedSearchTitle}>I'm looking for...</h3>
                <Grid container spacing={2}>
                  <Grid size={12}>
                    <MultipleSearchBar<AuthorDto>
                      label="Authors"
                      value={ownedBookAuthors}
                      onChange={(_event, v) => setOwnedBookAuthors(v)}
                      fetchMethod={fetchPagedAuthors}
                      placeholder="Search for authors..."
                      styles={styles}
                      getOptionLabel={(author) =>
                        author ? `${author.firstName} ${author.lastName}`.trim() : ''
                      }
                    />
                  </Grid>
                  <Grid size={{ xs: 12, sm: 6 }}>
                    <FormControl fullWidth variant="outlined">
                      <InputLabel id="owned-genre-label">Genre</InputLabel>
                      <Select
                        labelId="owned-genre-label"
                        value={ownedBookGenre?.id || ''}
                        onChange={handleOwnedGenreChange}
                        label="Genre"
                        className={styles.input}
                      >
                        {genres.map((genre) => (
                          <MenuItem key={genre.id} value={genre.id}>
                            {genre.name}
                          </MenuItem>
                        ))}
                      </Select>
                    </FormControl>
                  </Grid>
                  <Grid size={{ xs: 12, sm: 6 }}>
                    <SingleSearchBar<PublisherDto>
                      label="Publisher"
                      value={ownedBookPublisher}
                      onChange={(_event, v) => setOwnedBookPublisher(v)}
                      fetchMethod={fetchPagedPublishers}
                      placeholder="Search for publisher..."
                      styles={styles}
                      getOptionLabel={(publisher) => publisher?.name || ''}
                    />
                  </Grid>
                </Grid>
              </div>

              <div className={styles.advancedSearchSection}>
                <h3 className={styles.advancedSearchTitle}>I'm giving out...</h3>
                <Grid container spacing={2}>
                  <Grid size={12}>
                    <MultipleSearchBar<AuthorDto>
                      label="Authors"
                      value={wantedBookAuthors}
                      onChange={(_event, v) => setWantedBookAuthors(v)}
                      fetchMethod={fetchPagedAuthors}
                      placeholder="Search for authors..."
                      styles={styles}
                      getOptionLabel={(author) =>
                        author ? `${author.firstName} ${author.lastName}`.trim() : ''
                      }
                    />
                  </Grid>
                  <Grid size={{ xs: 12, sm: 6 }}>
                    <FormControl fullWidth variant="outlined">
                      <InputLabel id="wanted-genre-label">Genre</InputLabel>
                      <Select
                        labelId="wanted-genre-label"
                        value={wantedBookGenre?.id || ''}
                        onChange={handleWantedGenreChange}
                        label="Genre"
                        className={styles.input}
                      >
                        {genres.map((genre) => (
                          <MenuItem key={genre.id} value={genre.id}>
                            {genre.name}
                          </MenuItem>
                        ))}
                      </Select>
                    </FormControl>
                  </Grid>
                  <Grid size={{ xs: 12, sm: 6 }}>
                    <SingleSearchBar<PublisherDto>
                      label="Publisher"
                      value={wantedBookPublisher}
                      onChange={(_event, v) => setWantedBookPublisher(v)}
                      fetchMethod={fetchPagedPublishers}
                      placeholder="Search for publisher..."
                      styles={styles}
                      getOptionLabel={(publisher) => publisher?.name || ''}
                    />
                  </Grid>
                </Grid>
              </div>

              <div className={styles.orderingSettingsContainer}>
                <div className={styles.orderingSettings}>
                  <span><b>Sort by:</b></span>
                  <RadioGroup row value={sortBy} onChange={e => setSortBy(e.target.value)}>
                    <FormControlLabel value="lastOnlineDate" control={<Radio />} label="Last Online" />
                    <FormControlLabel value="registrationDate" control={<Radio />} label="Registration Date" />
                    <FormControlLabel value="userName" control={<Radio />} label="User Name" />
                  </RadioGroup>
                </div>
                <div className={styles.orderingSettings}>
                  <span><b>Sort direction:</b></span>
                  <RadioGroup row value={sortDirection} onChange={e => setSortDirection(e.target.value)}>
                    <FormControlLabel value="asc" control={<Radio />} label="Ascending" />
                    <FormControlLabel value="desc" control={<Radio />} label="Descending" />
                  </RadioGroup>
                </div>
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