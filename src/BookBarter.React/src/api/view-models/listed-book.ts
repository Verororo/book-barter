import type { OwnedBookDto, WantedBookDto } from "../generated/models"

export type ListedBook = {
  id: number
  authors: string
  title: string
  approved: boolean
  publicationYear: number
  publisherName: string
  bookStateName?: string
  color?: string
}

export const mapListedBookDtoToView = (dto: OwnedBookDto | WantedBookDto): ListedBook => {
  const book = dto.book!
  const authors = (book.authors ?? [])
    .map((a) => a.lastName)
    .join(", ")

  return {
    id: book.id!,
    title: book.title!,
    authors,
    approved: book.approved!,
    publicationYear: new Date(book.publicationDate!).getFullYear(),
    publisherName: book.publisherName ?? "",
    bookStateName: "bookStateName" in dto ? dto.bookStateName! : undefined,
  }
}
