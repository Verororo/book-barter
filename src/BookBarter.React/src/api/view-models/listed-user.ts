import type { ListedUserDto } from "../generated/models/listed-user-dto"
import { mapListedBookDtoToView, type ListedBook } from "./listed-book"

export type ListedUser = {
  userName: string
  cityName: string
  lastOnlineDate: string
  ownedBooks: ListedBook[]
  wantedBooks: ListedBook[]
}

export const mapListedUserDtoToView = (dto: ListedUserDto): ListedUser => {
  return {
    userName: dto.userName ?? "",
    cityName: dto.cityName ?? "",
    lastOnlineDate: dto.lastOnlineDate ?? new Date().toISOString(),
    ownedBooks: (dto.ownedBooks ?? []).map(mapListedBookDtoToView),
    wantedBooks: (dto.wantedBooks ?? []).map(mapListedBookDtoToView),
  }
}