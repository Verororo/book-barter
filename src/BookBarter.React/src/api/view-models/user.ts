import type { UserDto } from "../generated"
import { mapListedBookDtoToView, type ListedBook } from "./listed-book"

export type User = {
  id: number
  userName: string
  cityNameWithCountry: string
  cityId: number
  about: string
  lastOnlineDate: string
  registrationDate: string
  ownedBooks: ListedBook[]
  wantedBooks: ListedBook[]
}

export const mapUserDtoToView = (dto: UserDto): User => {
  return {
    id: dto.id!,
    userName: dto.userName ?? "",
    cityNameWithCountry: dto.city?.nameWithCountry ?? "",
    cityId: dto.city?.id ?? 0,
    about: dto.about ?? "",
    lastOnlineDate: dto.lastOnlineDate ?? new Date().toISOString(),
    registrationDate: dto.registrationDate ?? new Date().toISOString(),
    ownedBooks: (dto.ownedBooks ?? []).map(mapListedBookDtoToView),
    wantedBooks: (dto.wantedBooks ?? []).map(mapListedBookDtoToView)
  }
}