import { createContext, useContext, useState, useEffect, type ReactNode } from 'react';
import { jwtDecode } from 'jwt-decode';
import axios from 'axios';

type User = {
  userName: string;
  email: string;
  city: string;
  role: 'User' | 'Moderator' | 'Admin';
}

type LoginRequest = {
  email: string;
  password: string;
}

type RegisterRequest = {
  email: string;
  password: string;
  confirmPassword: string;
  userName: string;
  city: string;
}

type JwtPayload = {
  userName: string;
  email: string;
  city: string;
  role: string;
  exp: Date;
}

interface AuthContextType {
  user: User | null;
  isAuthenticated: boolean;
  login: (data: LoginRequest) => Promise<void>;
  register: (data: RegisterRequest) => Promise<void>;
  logout: () => void;
}

interface AuthProviderProps {
  children: ReactNode;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [user, setUser] = useState<User | null>(null);
  const isAuthenticated = user !== null;

  const API_BASE_URL = 'http://localhost:5123/api';
  const api = axios.create({
    baseURL: API_BASE_URL,
    headers: {
      'Content-Type': 'application/json',
    },
  });

  useEffect(() => {
    const token = localStorage.getItem('authToken');
    if (token) {
      try {
        const decoded = jwtDecode<JwtPayload>(token);

        if (decoded.exp && decoded.exp.getMilliseconds() < Date.now()) {
          const userData: User = {
            userName: decoded.userName,
            email: decoded.email,
            city: decoded.city,
            role: decoded.role as 'User' | 'Moderator' | 'Admin',
          };

          setUser(userData);
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
      const userData: User = {
        userName: decoded.userName,
        email: decoded.email,
        city: decoded.city,
        role: decoded.role as 'User' | 'Moderator' | 'Admin'
      };

      localStorage.setItem('authToken', token);
      api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
      setUser(userData);

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
    setUser(null);
    alert('You have logged out.');
  };

  const value: AuthContextType = {
    user,
    isAuthenticated,
    login,
    register,
    logout,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};