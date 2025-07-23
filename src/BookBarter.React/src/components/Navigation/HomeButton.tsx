import { Link } from 'react-router-dom';
import styles from './HomeButton.module.css';
import { Button } from '@mui/material';
import HomeIcon from '@mui/icons-material/Home';

const HomeButton = () => {
  return (
    <div className={styles.homeButtonBox}>
      <Button component={Link} to="/" variant="text" startIcon={<HomeIcon />}>
        Home
      </Button>
    </div>
  );
};

export default HomeButton;
