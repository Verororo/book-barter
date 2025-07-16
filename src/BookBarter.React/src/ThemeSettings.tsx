import { createTheme } from '@mui/material/styles';
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import { grey } from '@mui/material/colors';

export const theme = createTheme({
  palette: {
    background: {
      default: '#FFFBF2',
    },
    primary: {
      main: grey[800],
    },
  },
  components: {
    MuiCssBaseline: {
      styleOverrides: {
        body: {
          paddingTop: "24px",
          paddingBottom: "24px",
        },
      },
    },
    MuiPagination: {
      defaultProps: {
        variant: "outlined",
        shape: "rounded",
        color: "primary"
      },
      styleOverrides: {
        root: {
          marginTop: "24px"
        }
      }
    },
    MuiButton: {
      defaultProps: {
        disableElevation: true
      },
      styleOverrides: {
        root: {
          textTransform: "none"
        }
      }
    },
    MuiTab: {
      styleOverrides: {
        root: {
          textTransform: "none"
        }
      }
    },
    MuiToggleButton: {
      styleOverrides: {
        root: {
          textTransform: "none"
        }
      }
    }
  }
})

export default theme