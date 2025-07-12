import { createContext, useState, useEffect, type ReactNode } from 'react';
import { jwtDecode } from 'jwt-decode';
import axios from 'axios';

import type { UserAuthData } from './types/UserAuthData';
import type { LoginRequest } from './types/LoginRequest';
import type { RegisterRequest } from './types/RegisterRequest';
import type { JwtPayload } from './types/JwtPayload';

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

  const api = axios.create({
    baseURL: `${import.meta.env.VITE_API_BASE_URL}/api`,
    headers: {
      'Content-Type': 'application/json',
    },
  });

  useEffect(() => {
    const token = localStorage.getItem('authToken');
    if (token) {
      try {
        const decoded = jwtDecode<JwtPayload>(token);

        if (decoded.exp && decoded.exp < Date.now()) {
          const userAuthData: UserAuthData = {
            id: decoded.identifier,
            userName: decoded.userName,
            role: decoded.role as 'Moderator' | null,
          };

          setUserAuthData(userAuthData);
          api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        }
        else {
          localStorage.removeItem('authToken');
        }
      }
      catch (error) {
        localStorage.removeItem('authToken');
      }
    }
  }, []);

  const login = async (data: LoginRequest): Promise<void> => {
    try {
      const response = await api.post('/auth/login', data);
      const token = response.data;

      const decoded = jwtDecode<JwtPayload>(token);
      const userAuthData: UserAuthData = {
        id: decoded.identifier,
        userName: decoded.userName,
        role: decoded.role as 'Moderator' | null,
      };

      localStorage.setItem('authToken', token);
      api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
      setUserAuthData(userAuthData);

      alert('Login successful!');
    }
    catch (error) {
      alert('Login failed.');
      throw error;
    }
  };

  const register = async (data: RegisterRequest): Promise<void> => {
    try {
      await api.post('/auth/register', data);
      alert('Registration successful! You can now sign in.');
    }
    catch (error: any) {
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
    delete api.defaults.headers.common['Authorization'];
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