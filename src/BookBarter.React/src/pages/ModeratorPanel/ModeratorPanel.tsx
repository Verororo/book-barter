import { useState, useEffect, useCallback, useMemo } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../contexts/Auth/UseAuth';
import { Tab, ToggleButton, ToggleButtonGroup, TextField, Button, Collapse } from '@mui/material';
import TabContext from '@mui/lab/TabContext';
import TabList from '@mui/lab/TabList';
import TabPanel from '@mui/lab/TabPanel';
import HomeButton from '../../components/Navigation/HomeButton';
import LoadingSpinner from '../../components/LoadingSpinner/LoadingSpinner';
import Pagination from '../../components/Pagination/Pagination';
import SingleSearchBar from '../../components/SearchBars/SingleSearchBar';
import BooksList from '../../components/Moderation/BooksList';
import AuthorsList from '../../components/Moderation/AuthorsList';
import PublishersList from '../../components/Moderation/PublishersList';
import styles from './ModeratorPanel.module.css';
import type {
  AuthorDto,
  AuthorForModerationDto,
  BookForModerationDto,
  PublisherDto,
  PublisherForModerationDto
} from '../../api/generated';
import {
  fetchAuthorsForModeration,
  fetchPagedAuthors
} from '../../api/clients/author-client';
import {
  fetchBooksForModeration,
} from '../../api/clients/book-client';
import {
  fetchPagedPublishers,
  fetchPublishersForModeration
} from '../../api/clients/publisher-client';

const ITEMS_PER_PAGE = 10;

const ModeratorPanel = () => {
  const navigate = useNavigate();
  const { userAuthData } = useAuth();

  const [mainTab, setMainTab] = useState('1');
  const [showApproved, setShowApproved] = useState(true);
  const [loading, setLoading] = useState(false);

  const [books, setBooks] = useState<BookForModerationDto[]>([]);
  const [authors, setAuthors] = useState<AuthorForModerationDto[]>([]);
  const [publishers, setPublishers] = useState<PublisherForModerationDto[]>([]);

  const [totalBooks, setTotalBooks] = useState(0);
  const [totalAuthors, setTotalAuthors] = useState(0);
  const [totalPublishers, setTotalPublishers] = useState(0);

  const [currentPage, setCurrentPage] = useState(1);

  const [showAdvanced, setShowAdvanced] = useState(false);
  const [bookNameSearch, setBookNameSearch] = useState('');
  const [authorSearch, setAuthorSearch] = useState('');
  const [publisherSearch, setPublisherSearch] = useState('');
  const [selectedAuthor, setSelectedAuthor] = useState<AuthorDto | null>(null);
  const [selectedPublisher, setSelectedPublisher] = useState<PublisherDto | null>(null);

  const [searchParams, setSearchParams] = useState({
    bookName: '',
    authorName: '',
    publisherName: '',
    authorId: null as number | null,
    publisherId: null as number | null,
  });

  useEffect(() => {
    if (userAuthData?.role !== 'Moderator') {
      navigate('/');
    }
  }, [userAuthData, navigate]);

  useEffect(() => {
    setCurrentPage(1);
  }, [mainTab, showApproved]);

  const fetchBooks = useCallback(async () => {
    const data = await fetchBooksForModeration({
      pageNumber: currentPage,
      pageSize: ITEMS_PER_PAGE,
      orderByProperty: "addedDate",
      orderDirection: "desc",
      approved: showApproved,
      title: searchParams.bookName || undefined,
      authorId: searchParams.authorId || undefined,
      publisherId: searchParams.publisherId || undefined,
    });
    setBooks(data.items || []);
    setTotalBooks(data.total || 0);
  }, [currentPage, showApproved, searchParams.bookName, searchParams.authorId, searchParams.publisherId]);

  const fetchAuthors = useCallback(async () => {
    const data = await fetchAuthorsForModeration({
      pageNumber: currentPage,
      pageSize: ITEMS_PER_PAGE,
      orderByProperty: "addedDate",
      orderDirection: "asc",
      approved: showApproved,
      query: searchParams.authorName || undefined,
    });
    setAuthors(data.items || []);
    setTotalAuthors(data.total || 0);
  }, [currentPage, showApproved, searchParams.authorName]);

  const fetchPublishers = useCallback(async () => {
    const data = await fetchPublishersForModeration({
      pageNumber: currentPage,
      pageSize: ITEMS_PER_PAGE,
      orderByProperty: "addedDate",
      orderDirection: "asc",
      approved: showApproved,
      query: searchParams.publisherName || undefined,
    });
    setPublishers(data.items || []);
    setTotalPublishers(data.total || 0);
  }, [currentPage, showApproved, searchParams.publisherName]);

  const fetchData = useCallback(async () => {
    setLoading(true);
    try {
      switch (mainTab) {
        case "1":
          await fetchBooks();
          break;
        case "2":
          await fetchAuthors();
          break;
        case "3":
          await fetchPublishers();
          break;
      }
    } catch (error) {
      console.log(error);
    } finally {
      setLoading(false);
    }
  }, [mainTab, fetchBooks, fetchAuthors, fetchPublishers]);

  useEffect(() => {
    fetchData();
  }, [fetchData]);

  const handleSearch = useCallback(() => {
    setCurrentPage(1);
    setSearchParams({
      bookName: bookNameSearch,
      authorName: authorSearch,
      publisherName: publisherSearch,
      authorId: selectedAuthor?.id || null,
      publisherId: selectedPublisher?.id || null,
    });
  }, [bookNameSearch, authorSearch, publisherSearch, selectedAuthor, selectedPublisher]);

  const clearSearch = useCallback(() => {
    setBookNameSearch('');
    setAuthorSearch('');
    setPublisherSearch('');
    setSelectedAuthor(null);
    setSelectedPublisher(null);
    setSearchParams({
      bookName: '',
      authorName: '',
      publisherName: '',
      authorId: null,
      publisherId: null,
    });
  }, []);

  const scrollToTop = useCallback(() =>
    window.scrollTo({ top: 0, behavior: 'smooth' }), []);

  const handlePageChange = useCallback((page: number) => {
    setCurrentPage(page);
    setTimeout(scrollToTop, 50);
  }, [scrollToTop]);

  const getCurrentTotal = useMemo(() => {
    switch (mainTab) {
      case "1": return totalBooks;
      case "2": return totalAuthors;
      case "3": return totalPublishers;
      default: return 0;
    }
  }, [mainTab, totalBooks, totalAuthors, totalPublishers]);

  const handleBookNameChange = useCallback((e: React.ChangeEvent<HTMLInputElement>) => {
    setBookNameSearch(e.target.value);
  }, []);

  const handleAuthorChange = useCallback((e: React.ChangeEvent<HTMLInputElement>) => {
    setAuthorSearch(e.target.value);
  }, []);

  const handlePublisherChange = useCallback((e: React.ChangeEvent<HTMLInputElement>) => {
    setPublisherSearch(e.target.value);
  }, []);

  const handleToggleAdvanced = useCallback(() => {
    setShowAdvanced(prev => !prev);
  }, []);

  return (
    <div className={styles.moderatorBox}>
      <HomeButton />

      <TabContext value={mainTab}>
        <TabList
          onChange={(_event, newValue: string) => setMainTab(newValue)}
          className={styles.tabsContainer}
          variant='fullWidth'
        >
          <Tab label='Books' value='1'></Tab>
          <Tab label='Authors' value='2'></Tab>
          <Tab label='Publishers' value='3'></Tab>
        </TabList>

        <div className={styles.filterToggle}>
          <ToggleButtonGroup
            value={showApproved ? 'approved' : 'unapproved'}
            exclusive
            onChange={(_, val) => val && setShowApproved(val === 'approved')}
            size="small"
          >
            <ToggleButton value="approved">Approved</ToggleButton>
            <ToggleButton value="unapproved">Unapproved</ToggleButton>
          </ToggleButtonGroup>
        </div>

        <div className={styles.search}>
          <div className={styles.searchBarsContainer}>
            {mainTab === '1' && (
              <>
                <TextField
                  className={styles.input}
                  label="Search by book name..."
                  value={bookNameSearch}
                  onChange={handleBookNameChange}
                  variant="outlined"
                  fullWidth
                />
                <Button variant="text" onClick={handleToggleAdvanced}>
                  Advanced
                </Button>
              </>
            )}
            {mainTab === '2' && (
              <TextField
                className={styles.input}
                label="Search by author name..."
                value={authorSearch}
                onChange={handleAuthorChange}
                variant="outlined"
                fullWidth
              />
            )}
            {mainTab === '3' && (
              <TextField
                className={styles.input}
                label="Search by publisher name..."
                value={publisherSearch}
                onChange={handlePublisherChange}
                variant="outlined"
                fullWidth
              />
            )}
          </div>

          {mainTab === '1' && (
            <Collapse in={showAdvanced}>
              <div className={styles.advancedSearchContainer}>
                <SingleSearchBar
                  label="Search by Author..."
                  value={selectedAuthor}
                  onChange={(_event, newValue) => setSelectedAuthor(newValue)}
                  fetchMethod={fetchPagedAuthors}
                  placeholder="Type author name..."
                  variant="outlined"
                  styles={styles}
                  getOptionLabel={(option) =>
                    option ? `${option.firstName} ${option.lastName}`.trim() : ''
                  }
                />
                <SingleSearchBar
                  label="Search by Publisher..."
                  value={selectedPublisher}
                  onChange={(_event, newValue) => setSelectedPublisher(newValue)}
                  fetchMethod={fetchPagedPublishers}
                  placeholder="Type publisher name..."
                  variant="outlined"
                  styles={styles}
                  getOptionLabel={(option) => option?.name || ''}
                />
              </div>
            </Collapse>
          )}

          <div className={styles.controlButtons}>
            <Button variant="outlined" onClick={clearSearch}>
              Clear
            </Button>
            <Button variant="contained" color="primary" onClick={handleSearch}>
              Search
            </Button>
          </div>
        </div>

        <div className={styles.contentContainer}>
          {loading ? (
            <LoadingSpinner />
          ) : (
            <>
              <TabPanel value="1" style={{ padding: 0 }}>
                <BooksList
                  books={books}
                  onRefresh={fetchBooks}
                  hasPagination={totalBooks > ITEMS_PER_PAGE}
                />
              </TabPanel>
              <TabPanel value="2" style={{ padding: 0 }}>
                <AuthorsList
                  authors={authors}
                  onRefresh={fetchAuthors}
                  hasPagination={totalAuthors > ITEMS_PER_PAGE}
                />
              </TabPanel>
              <TabPanel value="3" style={{ padding: 0 }}>
                <PublishersList
                  publishers={publishers}
                  onRefresh={fetchPublishers}
                  hasPagination={totalPublishers > ITEMS_PER_PAGE}
                />
              </TabPanel>
            </>
          )}
        </div>

        <Pagination
          pageNumber={currentPage}
          pageSize={ITEMS_PER_PAGE}
          total={getCurrentTotal}
          onPageChange={handlePageChange}
        />
      </TabContext>
    </div>
  );
};

export default ModeratorPanel;