import { createContext, useState, useEffect, type ReactNode } from 'react';
import { jwtDecode } from 'jwt-decode';

import type { UserAuthData } from './types/UserAuthData';
import type { LoginRequest } from './types/LoginRequest';
import type { RegisterRequest } from './types/RegisterRequest';
import type { JwtPayload } from './types/JwtPayload';
import type { LoginCommand, RegisterCommand } from '../../api/generated';
import { sendLoginCommand, sendRegisterCommand } from '../../api/clients/auth-client';

interface AuthContextType {
  userAuthData: UserAuthData | null;
  isAuthenticated: boolean;
  login: (data: LoginRequest) => Promise<void>;
  register: (data: RegisterRequest) => Promise<void>;
  logout: () => void;
}

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [userAuthData, setUserAuthData] = useState<UserAuthData | null>(null);
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

      alert('Login successful!');
    } catch (error) {
      alert('Login failed.');
      throw error
    }
  };

  const register = async (data: RegisterCommand): Promise<void> => {
    try {
      await sendRegisterCommand(data)
      alert('Registration successful! You can now sign in.');
    } catch (error: any) {
      if (error.response?.data?.messages) {
        alert('Registration failed: ' + error.response.data.messages.join(', '));
      } else {
        alert('Registration failed. Please try again later.');
      }
      throw error;
    }
  };

  const logout = () => {
    localStorage.removeItem('authToken');
    setUserAuthData(null);
    alert('You have logged out.');
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