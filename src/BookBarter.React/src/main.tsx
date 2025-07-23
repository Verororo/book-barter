import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import './api/interceptors/request-interceptor';
import App from './App.tsx';

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
  </StrictMode>,
);
