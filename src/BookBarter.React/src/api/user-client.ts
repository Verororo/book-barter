import { UsersApi } from "./generated/apis/users-api"
import { Configuration } from "./generated/configuration"
import type { GetPagedUsersQuery } from "./generated/models/get-paged-users-query"
import { mapListedUserPaginatedResultDtoToView, type ListedUserPaginated } from "./view-models/listed-user-paginated-result"

const cfg = new Configuration({ basePath: `${import.meta.env.VITE_API_BASE_URL}` })
const usersApi = new UsersApi(cfg)

export const fetchListedUsersPaginated = async (
  query: GetPagedUsersQuery
): Promise<ListedUserPaginated> => {
  const response = await usersApi.apiUsersPagedPost(query);

  return mapListedUserPaginatedResultDtoToView(response.data);
};