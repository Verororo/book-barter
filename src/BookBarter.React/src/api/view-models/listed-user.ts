import type { ListedUserDto } from '../generated/models/listed-user-dto';
import { mapListedBookDtoToView, type ListedBook } from './listed-book';

export type ListedUser = {
  id: number;
  userName: string;
  cityNameWithCountry: string;
  lastOnlineDate: string;
  ownedBooks: ListedBook[];
  wantedBooks: ListedBook[];
};

export const mapListedUserDtoToView = (dto: ListedUserDto): ListedUser => {
  return {
    id: dto.id ?? 0,
    userName: dto.userName ?? '',
    cityNameWithCountry: dto.cityNameWithCountry ?? '',
    lastOnlineDate: dto.lastOnlineDate ?? new Date().toISOString(),
    ownedBooks: (dto.ownedBooks ?? []).map(mapListedBookDtoToView),
    wantedBooks: (dto.wantedBooks ?? []).map(mapListedBookDtoToView),
  };
};
