import type { BookDtoPaginatedResult, ListedBookDto } from "../generated"

export const mapAutocompleteBookPaginatedDtoToView = (dto: BookDtoPaginatedResult): ListedBookDto[] => {
  return dto.items ? dto.items : []
}