import axios from "axios";

axios.interceptors.request.use(config => {
  const token = localStorage.getItem('authToken');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  
  return config;
}, error => {
  return Promise.reject(error);
});

// SUGGESTION: If you want to handle errors globally, you can add an interceptor for responses as well.