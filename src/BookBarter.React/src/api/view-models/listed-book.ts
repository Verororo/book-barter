import type { OwnedBookDto, WantedBookDto } from "../generated/models"

export type ListedBook = {
  authors: string
  title: string
  publicationYear: number
  publisherName: string
  bookStateName?: string
}

export const mapListedBookDtoToView = (dto: OwnedBookDto | WantedBookDto): ListedBook => {
  const book = dto.book!
  const authors = (book.authors ?? [])
    .map((a) => a.lastName)
    .join(", ")

  return {
    title: book.title!,
    authors,
    publicationYear: new Date(book.publicationDate!).getFullYear(),
    publisherName: book.publisherName ?? "",
    bookStateName: "bookStateName" in dto ? dto.bookStateName! : undefined,
  }
}
