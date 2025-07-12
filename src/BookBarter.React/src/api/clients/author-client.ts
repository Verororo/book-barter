import { requestConfig } from "./common";
import { AuthorsApi, type AuthorDto, type CreateAuthorCommand } from "../generated";

const authorsApi = new AuthorsApi(requestConfig)

export const createAuthorCommand = async (command: CreateAuthorCommand): Promise<number | undefined> => {
  try {
    const response = await authorsApi.apiAuthorsPost(command)
    return response.data
  }
  catch (error) {
    console.error(error)
    return undefined
  }
}

export const fetchPagedAuthors = async (query: string): Promise<AuthorDto[]> => {
  try {
    const response = await authorsApi.apiAuthorsPagedPost({
      pageSize: 10,
      pageNumber: 1,
      orderByProperty: "lastName",
      orderDirection: "asc",
      query: query,
    })

    return response.data.items ?? [];

  } catch (error) {
    console.error(error);
    return [];
  }
}