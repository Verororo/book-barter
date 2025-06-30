
import Navigation from './components/Navigation/Navigation'
import theme from './ThemeSettings'
import CssBaseline from '@mui/material/CssBaseline'

import { lazy, Suspense } from 'react'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'

import { ThemeProvider } from '@mui/material/styles'
import { AuthProvider } from './contexts/Auth/AuthContext';
import LoadingSpinner from './components/LoadingSpinner/LoadingSpinner'

const UserItemContainer = lazy(() => import('./pages/Home/UserItemContainer'))
const Auth = lazy(() => import('./pages/Auth/Auth'))

function App() {
  return (
    <AuthProvider>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Router>
          <Navigation />
          <Suspense fallback={<LoadingSpinner />}>
            <Routes>
              <Route path='/' element={<UserItemContainer />} />
              <Route path='/auth' element={<Auth />} />
            </Routes>
          </Suspense>
        </Router>
      </ThemeProvider>
    </AuthProvider>
  )
}

export default App
