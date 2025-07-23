import axios, { type AxiosResponse, AxiosError } from 'axios';

if (import.meta.env.DEV) {
  axios.interceptors.response.use(
    (response: AxiosResponse) => {
      return response;
    },
    (error: AxiosError) => {
      if (error.response && error.response.status >= 400) {
        console.error('Intercepted error response:', error.response);
      }
      return Promise.reject(error);
    },
  );
}
