import styles from './Pagination.module.css';

interface PaginationProps {
  pageNumber: number;
  pageSize: number;
  total: number;
  onPageChange: (page: number) => void;
}

const Pagination = ({
  pageNumber,
  pageSize,
  total,
  onPageChange,
}: PaginationProps) => {
  const totalPages = Math.ceil(total / pageSize);

  if (totalPages <= 1) {
    return null;
  }

  const pages = [];
  const maxVisiblePages = 7;

  const scrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: 'smooth',
    });
  };

  const handlePageClick = (page: number) => {
    if (
      onPageChange &&
      page !== pageNumber &&
      page >= 1 &&
      page <= totalPages
    ) {
      onPageChange(page);
      setTimeout(scrollToTop, 50);
    }
  };

  let startPage = Math.max(1, pageNumber - Math.floor(maxVisiblePages / 2));
  let endPage = Math.min(totalPages, startPage + maxVisiblePages - 1);

  if (endPage - startPage + 1 < maxVisiblePages) {
    startPage = Math.max(1, endPage - maxVisiblePages + 1);
  }

  if (pageNumber > 1) {
    pages.push(
      <button
        key="prev"
        className={styles.paginationButton}
        onClick={() => handlePageClick(pageNumber - 1)}
      >
        Previous
      </button>,
    );
  }

  if (startPage > 1) {
    pages.push(
      <button
        key={1}
        className={styles.paginationButton}
        onClick={() => handlePageClick(1)}
      >
        1
      </button>,
    );

    if (startPage > 2) {
      pages.push(
        <span key="ellipsis1" className={styles.ellipsis}>
          ...
        </span>,
      );
    }
  }

  for (let i = startPage; i <= endPage; i++) {
    pages.push(
      <button
        key={i}
        className={`${styles.paginationButton} ${
          i === pageNumber ? styles.active : ''
        }`}
        disabled={i === pageNumber}
        onClick={() => handlePageClick(i)}
      >
        {i}
      </button>,
    );
  }

  if (endPage < totalPages) {
    if (endPage < totalPages - 1) {
      pages.push(
        <span key="ellipsis2" className={styles.ellipsis}>
          ...
        </span>,
      );
    }

    pages.push(
      <button
        key={totalPages}
        className={styles.paginationButton}
        onClick={() => handlePageClick(totalPages)}
      >
        {totalPages}
      </button>,
    );
  }

  if (pageNumber < totalPages) {
    pages.push(
      <button
        key="next"
        className={styles.paginationButton}
        onClick={() => handlePageClick(pageNumber + 1)}
      >
        Next
      </button>,
    );
  }

  return (
    <div className={styles.pagination}>
      <div className={styles.paginationInfo}>
        Showing {(pageNumber - 1) * pageSize + 1} to{' '}
        {Math.min(pageNumber * pageSize, total)} of {total} results
      </div>
      <div className={styles.paginationButtons}>{pages}</div>
    </div>
  );
};

export default Pagination;
