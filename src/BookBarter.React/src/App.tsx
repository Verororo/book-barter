import Navigation from './components/Navigation/Navigation';
import theme from './ThemeSettings';
import CssBaseline from '@mui/material/CssBaseline';

import { lazy, Suspense } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import { ThemeProvider } from '@mui/material/styles';
import { AuthProvider } from './contexts/Auth/AuthContext';
import LoadingSpinner from './components/LoadingSpinner/LoadingSpinner';
import { NotificationProvider } from './contexts/Notification/NotificationContext';

const Home = lazy(() => import('./pages/Home/Home'));
const Auth = lazy(() => import('./pages/Auth/Auth'));
const UserProfile = lazy(() => import('./pages/UserProfile/UserProfile'));
const OwnProfile = lazy(() => import('./pages/OwnProfile/OwnProfile'));
const ModeratorPanel = lazy(
  () => import('./pages/ModeratorPanel/ModeratorPanel'),
);
const Messages = lazy(() => import('./pages/Messages/Messages'));

function App() {
  return (
    <NotificationProvider>
      <AuthProvider>
        <ThemeProvider theme={theme}>
          <CssBaseline />
          <Router>
            <Navigation />
            <Suspense fallback={<LoadingSpinner />}>
              <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/auth" element={<Auth />} />
                <Route path="/users/:userId" element={<UserProfile />} />
                <Route path="/users/me" element={<OwnProfile />} />
                <Route path="/moderator" element={<ModeratorPanel />} />
                <Route path="/messages" element={<Messages />} />
              </Routes>
            </Suspense>
          </Router>
        </ThemeProvider>
      </AuthProvider>
    </NotificationProvider>
  );
}

export default App;
