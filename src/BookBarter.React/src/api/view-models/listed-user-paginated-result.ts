import type { ListedUserDtoPaginatedResult } from '../generated/models/listed-user-dto-paginated-result';
import { mapListedUserDtoToView, type ListedUser } from './listed-user';

export type ListedUserPaginatedResult = {
  pageNumber: number;
  pageSize: number;
  total: number;
  items: ListedUser[];
};

export const mapListedUserPaginatedResultDtoToView = (
  dto: ListedUserDtoPaginatedResult,
): ListedUserPaginatedResult => {
  return {
    pageNumber: dto.pageNumber ?? 1,
    pageSize: dto.pageSize ?? 0,
    total: dto.total ?? 0,
    items: (dto.items ?? []).map(mapListedUserDtoToView),
  };
};
