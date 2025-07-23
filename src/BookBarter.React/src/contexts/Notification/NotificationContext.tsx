import { createContext, useState, type ReactNode } from 'react';
import { Alert, Snackbar } from '@mui/material';

type AlertSeverity = 'success' | 'error' | 'warning' | 'info';

interface NotificationContextType {
  showNotification: (message: string, severity?: AlertSeverity) => void;
}

type NotificationProviderProps = {
  children: ReactNode;
};

type NotificationState = {
  open: boolean;
  message: string;
  severity: AlertSeverity;
};

export const NotificationContext = createContext<
  NotificationContextType | undefined
>(undefined);

export const NotificationProvider = ({
  children,
}: NotificationProviderProps) => {
  const [notification, setNotification] = useState<NotificationState>({
    open: false,
    message: '',
    severity: 'info',
  });

  const showNotification = (
    message: string,
    severity: AlertSeverity = 'info',
  ) => {
    setNotification({
      open: true,
      message,
      severity,
    });
  };

  const handleClose = () => {
    setNotification((prev) => ({ ...prev, open: false }));
  };

  return (
    <NotificationContext.Provider value={{ showNotification }}>
      {children}
      <Snackbar
        open={notification.open}
        autoHideDuration={5000}
        onClose={handleClose}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
        sx={{ zIndex: (theme) => theme.zIndex.snackbar + 1 }}
      >
        <Alert
          onClose={handleClose}
          severity={notification.severity}
          sx={{ width: '100%' }}
          elevation={0}
          variant={'filled'}
        >
          {notification.message}
        </Alert>
      </Snackbar>
    </NotificationContext.Provider>
  );
};
