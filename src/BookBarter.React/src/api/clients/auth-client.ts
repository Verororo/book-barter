import { AuthApi, type LoginCommand, type RegisterCommand } from "../generated";
import { requestConfig } from "./common";

const authApi = new AuthApi(requestConfig)

export const sendLoginCommand = async (
  command: LoginCommand
): Promise<string> => {

  // FIX: remove unnecessary try-catch block
  // SUGGESTION: Handle errors in the calling function or globally using interceptors.

    const response = await authApi.apiAuthLoginPost(command)
    return response.data
}

export const sendRegisterCommand = async (
  command: RegisterCommand
): Promise<void> => {
  try {
    await authApi.apiAuthRegisterPost(command)
  } catch (error) {
    console.error(error)
    throw error
  }
}