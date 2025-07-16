import type { BookDtoPaginatedResult, ListedBookDto } from "../generated"

export const mapAutocompleteBookPaginatedResultDtoToView = (dto: BookDtoPaginatedResult): ListedBookDto[] => (
  dto.items ?? []
)