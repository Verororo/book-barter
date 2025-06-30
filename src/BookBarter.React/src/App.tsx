
import Navigation from './components/Navigation/Navigation'
import UserItemContainer from './pages/Home/UserItemContainer'

import theme from './ThemeSettings'
import { ThemeProvider } from '@mui/material/styles'
import CssBaseline from '@mui/material/CssBaseline'

import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'

import Auth from './pages/Auth/Auth'
import { AuthProvider } from './contexts/Auth/AuthContext';

// add lazy loading for separate pages
function App() {
  return (
    <AuthProvider>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Router>
          <Navigation/>
          <Routes>
            <Route path='/' element={<UserItemContainer />} />
            <Route path='/auth' element={<Auth />} />
          </Routes>
        </Router>
      </ThemeProvider>
    </AuthProvider>
  )
}

export default App
