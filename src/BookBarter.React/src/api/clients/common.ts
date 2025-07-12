import { Configuration } from "../generated/configuration"

export const requestConfig = new Configuration({
  basePath: `${import.meta.env.VITE_API_BASE_URL}`
})