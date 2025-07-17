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
  try {
    const response = await usersApi.apiUsersPagedPost(query)
    return mapListedUserPaginatedResultDtoToView(response.data)
  } catch (error) {
    console.error(error)
    throw error
  }
};

export const fetchUserById = async (
  id: number,
  excludeUnapprovedBooks: boolean
): Promise<User> => {
  try {
    const response = await usersApi.apiUsersIdGet(id, excludeUnapprovedBooks)
    return mapUserDtoToView(response.data)
  } catch (error) {
    console.error(error)
    throw error
  }

}

export const updateUser = async (
  query: UpdateUserCommand
) => {
  try {
    await usersApi.apiUsersMePut(query)
  } catch (error) {
    console.error(error)
    throw error
  }
}

export const deleteUser = async (
  id: number
) => {
  try {
    await usersApi.apiUsersIdDelete(id)
  } catch (error) {
    console.error(error)
    throw error
  }
}

export const addBookToOwned = async (
  query: AddOwnedBookCommand
) => {
  try {
    await usersApi.apiUsersMeOwnedBooksPost(query)
  } catch (error) {
    console.error(error)
    throw error
  }
}

export const deleteBookFromOwned = async (
  query: DeleteOwnedBookCommand
) => {
  try {
    await usersApi.apiUsersMeOwnedBooksBookIdDelete(query.bookId!, query);
  } catch (error) {
    console.error(error)
    throw error
  }
}

export const addBookToWanted = async (
  query: AddWantedBookCommand
) => {
  try {
    await usersApi.apiUsersMeWantedBooksPost(query)
  } catch (error) {
    console.error(error)
    throw error
  }
}

export const deleteBookFromWanted = async (
  query: DeleteWantedBookCommand
) => {
  try {
    await usersApi.apiUsersMeWantedBooksBookIdDelete(query.bookId!, query)
  } catch (error) {
    console.error(error)
    throw error
  }
}