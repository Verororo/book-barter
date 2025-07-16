import { requestConfig } from "./common";
import { AuthorsApi, type AuthorDto, type AuthorForModerationDtoPaginatedResult, type CreateAuthorCommand, type GetPagedAuthorsForModerationQuery, type UpdateAuthorCommand } from "../generated";

const authorsApi = new AuthorsApi(requestConfig)

export const createAuthorCommand = async (
  command: CreateAuthorCommand
): Promise<number | undefined> => {
  try {
    const response = await authorsApi.apiAuthorsPost(command)
    return response.data
  }
  catch (error) {
    console.error(error)
    return undefined
  }
}

export const approveAuthor = async (
  id: number
) => {
  await authorsApi.apiAuthorsIdApprovePut(id)
}

export const updateAuthor = async (
  query: UpdateAuthorCommand
) => {
  await authorsApi.apiAuthorsIdPut(query.id!, query)
}

export const deleteAuthor = async (
  id: number
) => {
  await authorsApi.apiAuthorsIdDelete(id)
}

export const fetchAuthorsForModeration = async (
  query: GetPagedAuthorsForModerationQuery
): Promise<AuthorForModerationDtoPaginatedResult> => {
  const response = await authorsApi.apiAuthorsPagedModeratedPost(query);

  return response.data;
}

export const fetchPagedAuthors = async (
  query: string,
  idsToSkip?: number[]
): Promise<AuthorDto[]> => {
  try {
    const response = await authorsApi.apiAuthorsPagedPost({
      pageSize: 10,
      pageNumber: 1,
      orderByProperty: "lastName",
      orderDirection: "asc",
      query,
      idsToSkip
    })

    return response.data.items ?? [];

  } catch (error) {
    console.error(error);
    return [];
  }
}