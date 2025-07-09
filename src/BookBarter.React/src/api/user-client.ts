import type { UpdateUserCommand } from "./generated"
import { UsersApi } from "./generated/apis/users-api"
import { Configuration } from "./generated/configuration"
import type { GetPagedUsersQuery } from "./generated/models/get-paged-users-query"
import { mapListedUserPaginatedResultDtoToView, type ListedUserPaginated } from "./view-models/listed-user-paginated-result"
import { mapUserDtoToView, type User } from "./view-models/user"

const cfg = new Configuration({ basePath: `${import.meta.env.VITE_API_BASE_URL}` })
const usersApi = new UsersApi(cfg)

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