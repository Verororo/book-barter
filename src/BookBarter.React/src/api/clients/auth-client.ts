import { AuthApi, type LoginCommand, type RegisterCommand } from "../generated";
import { requestConfig } from "./common";

const authApi = new AuthApi(requestConfig)

export const sendLoginCommand = async (
  command: LoginCommand
): Promise<string> => {
  const response = await authApi.apiAuthLoginPost(command)
  return response.data
}

export const sendRegisterCommand = async (
  command: RegisterCommand
): Promise<void> => {
  await authApi.apiAuthRegisterPost(command)
}