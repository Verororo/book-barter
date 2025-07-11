import { requestConfig } from "./common"
import type { AddOwnedBookCommand, DeleteOwnedBookCommand, UpdateUserCommand } from "./generated"
import { UsersApi } from "./generated/apis/users-api"
import type { GetPagedUsersQuery } from "./generated/models/get-paged-users-query"
import { mapListedUserPaginatedResultDtoToView, type ListedUserPaginated } from "./view-models/listed-user-paginated-result"
import { mapUserDtoToView, type User } from "./view-models/user"

const usersApi = new UsersApi(requestConfig)

export const fetchListedUsersPaginated = async (
  query: GetPagedUsersQuery
): Promise<ListedUserPaginated> => {
  const response = await usersApi.apiUsersPagedPost(query)

  return mapListedUserPaginatedResultDtoToView(response.data)
};

export const fetchUserById = async (
  id: number
): Promise<User> => {
  const response = await usersApi.apiUsersIdGet(id)

  return mapUserDtoToView(response.data)
}

export const updateUser = async (
  query: UpdateUserCommand
) => {
  await usersApi.apiUsersIdPut(query.id!, query)
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