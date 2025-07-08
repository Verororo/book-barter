import type { BookDtoPaginatedResult } from "../generated"

export type AutocompleteBookItem = {
  id: number;
  info: string;
}

export const mapAutocompleteBookPaginatedDtoToView = (dto: BookDtoPaginatedResult): AutocompleteBookItem[] => {
  return dto.items!.map(book => {
    const authorName = book.authors!.length == 1
      ? `${book.authors![0].firstName} ${book.authors![0].lastName}`.trim()
      : book.authors?.map((a) => a.lastName).join(", ")
    return {
      id: book.id!,
      info: `${authorName}. ${book.title}`
    };
  });
}