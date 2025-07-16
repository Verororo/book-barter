import { requestConfig } from "./common"
import type { AddOwnedBookCommand, AddWantedBookCommand, DeleteOwnedBookCommand, DeleteWantedBookCommand, UpdateUserCommand } from "../generated"
import { UsersApi } from "../generated/apis/users-api"
import type { GetPagedUsersQuery } from "../generated/models/get-paged-users-query"
import { mapListedUserPaginatedResultDtoToView, type ListedUserPaginatedResult } from "../view-models/listed-user-paginated-result"
import { mapUserDtoToView, type User } from "../view-models/user"

const usersApi = new UsersApi(requestConfig)

export const fetchListedUsersPaginatedResult = async (
  query: GetPagedUsersQuery
): Promise<ListedUserPaginatedResult> => {
  const response = await usersApi.apiUsersPagedPost(query)

  return mapListedUserPaginatedResultDtoToView(response.data)
};

export const fetchUserById = async (
  id: number,
  excludeUnapprovedBooks: boolean
): Promise<User> => {
  const response = await usersApi.apiUsersIdGet(id, excludeUnapprovedBooks)

  return mapUserDtoToView(response.data)
}

export const updateUser = async (
  query: UpdateUserCommand
) => {
  await usersApi.apiUsersMePut(query)
}

export const deleteUser = async (
  id: number
) => {
  await usersApi.apiUsersIdDelete(id)
}

export const addBookToOwned = async (
  query: AddOwnedBookCommand
) => {
  await usersApi.apiUsersMeOwnedBooksPost(query)
}

export const deleteBookFromOwned = async (
  query: DeleteOwnedBookCommand
) => {
  await usersApi.apiUsersMeOwnedBooksBookIdDelete(query.bookId!, query);
}

export const addBookToWanted = async (
  query: AddWantedBookCommand
) => {
  await usersApi.apiUsersMeWantedBooksPost(query)
}

export const deleteBookFromWanted = async (
  query: DeleteWantedBookCommand
) => {
  await usersApi.apiUsersMeWantedBooksBookIdDelete(query.bookId!, query)
}