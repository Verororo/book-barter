import { Configuration } from "./generated/configuration"

const getToken = () => {
  const token = localStorage.getItem('authToken')
  return token ?? ''
}

export const requestConfig = new Configuration({
  basePath: `${import.meta.env.VITE_API_BASE_URL}`,
  accessToken: getToken
})