import { UsersApi } from "./generated/apis/users-api"
import { Configuration } from "./generated/configuration"
import { mapListedUserPaginatedResultDtoToView, type ListedUserPaginated } from "./view-models/listed-user-paginated-result"

const cfg = new Configuration({ basePath: `${import.meta.env.VITE_API_BASE_URL}` })
const usersApi = new UsersApi(cfg)

export const fetchListedUsersPaginated = async (
  pageNumber: number, 
  pageSize: number,
  orderByProperty: string = 'lastOnlineDate',
  orderDirection: string = 'asc'
) : Promise<ListedUserPaginated> => {
  const response = await usersApi.apiUsersPagedPost({
    pageNumber,
    pageSize,
    orderByProperty,
    orderDirection,
  })

  return mapListedUserPaginatedResultDtoToView(response.data)
}