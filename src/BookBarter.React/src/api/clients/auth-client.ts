import { AuthApi, type LoginCommand, type RegisterCommand } from "../generated";
import { requestConfig } from "./common";

const authApi = new AuthApi(requestConfig)

export const sendLoginCommand = async (
  command: LoginCommand
): Promise<string> => {
  try {
    const response = await authApi.apiAuthLoginPost(command)
    return response.data
  }
  catch (error) {
    console.error(error)
    throw error
  }
}

export const sendRegisterCommand = async (
  command: RegisterCommand
): Promise<void> => {
  try {
    await authApi.apiAuthRegisterPost(command)
  }
  catch (error) {
    console.error(error)
    throw error
  }
}