
import './App.css'
import Navigation from './components/Navigation/Navigation'
import UserItemContainer from './components/UserItem/UserItemContainer'
import { ThemeProvider, createTheme } from '@mui/material/styles';
import { grey } from '@mui/material/colors';
import CssBaseline from '@mui/material/CssBaseline';

// place in separate file
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';

const theme = createTheme({
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
    }
  }
})
// 

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Navigation/>
      <UserItemContainer/>
    </ThemeProvider>
  )
}

export default App
