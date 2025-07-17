import { createContext, useState, useEffect, type ReactNode } from 'react';
import { jwtDecode } from 'jwt-decode';

import type { UserAuthData } from './types/UserAuthData';
import type { JwtPayload } from './types/JwtPayload';
import type { LoginCommand, RegisterCommand } from '../../api/generated';
import { sendLoginCommand, sendRegisterCommand } from '../../api/clients/auth-client';
import { useNotification } from '../Notification/UseNotification';

interface AuthContextType {
  userAuthData: UserAuthData | null;
  isAuthenticated: boolean;
  login: (data: LoginCommand) => Promise<void>;
  register: (data: RegisterCommand) => Promise<void>;
  logout: () => void;
}

type AuthProviderProps = {
  children: ReactNode;
}

export const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [userAuthData, setUserAuthData] = useState<UserAuthData | null>(null);
  const { showNotification } = useNotification();
  const isAuthenticated = userAuthData !== null;

  useEffect(() => {
    const token = localStorage.getItem('authToken');
    if (token) {
      try {
        const decoded = jwtDecode<JwtPayload>(token);

        const expiresAt = decoded.exp ? decoded.exp * 1000 : 0;

        if (expiresAt > Date.now()) {
          const userAuthData: UserAuthData = {
            id: decoded.identifier,
            userName: decoded.userName,
            role: decoded.role as 'Moderator' | null,
          };

          setUserAuthData(userAuthData);
        } else {
          localStorage.removeItem('authToken');
        }
      } catch (error) {
        localStorage.removeItem('authToken');
      }
    }
  }, []);

  const login = async (data: LoginCommand): Promise<void> => {
    try {
      const token = await sendLoginCommand(data)
      // If the credentials are wrong, sendLoginCommand will throw an exception

      const decoded = jwtDecode<JwtPayload>(token);
      const userAuthData: UserAuthData = {
        id: decoded.identifier,
        userName: decoded.userName,
        role: decoded.role as 'Moderator' | null,
      };

      localStorage.setItem('authToken', token);
      setUserAuthData(userAuthData);

      showNotification('Login successful!', 'success');
    } catch (error) {
      showNotification('Login failed.', 'error');
      throw error
    }
  };

  const register = async (data: RegisterCommand): Promise<void> => {
    try {
      await sendRegisterCommand(data)
      showNotification('Registration successful! You can now sign in.', 'success');
    } catch (error: any) {
      if (error.response?.data?.messages) {
        showNotification('Registration failed: ' + error.response.data.messages.join(', '), 'error');
      } else {
        showNotification('Registration failed. Please try again later.', 'error');
      }
      throw error;
    }
  };

  const logout = () => {
    localStorage.removeItem('authToken');
    setUserAuthData(null);
    showNotification('You have logged out.', 'info');
  };

  const value: AuthContextType = {
    userAuthData,
    isAuthenticated,
    login,
    register,
    logout,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};