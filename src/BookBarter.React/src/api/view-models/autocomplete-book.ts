import type { BookDtoPaginatedResult, ListedBookDto } from "../generated"

export const mapAutocompleteBookPaginatedResultDtoToView = (dto: BookDtoPaginatedResult): ListedBookDto[] => {
  return dto.items ?? []
}