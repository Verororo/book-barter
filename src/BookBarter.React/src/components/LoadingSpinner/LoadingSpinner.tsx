import CircularProgress from '@mui/material/CircularProgress';
import styles from './LoadingSpinner.module.css';

export const LoadingSpinner = () => (
  <div className={styles.loadingSpinner}>
    <CircularProgress />
  </div>
);

export default LoadingSpinner;
