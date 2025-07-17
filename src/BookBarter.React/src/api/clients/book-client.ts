import { requestConfig } from "./common";
import type { BookForModerationDtoPaginatedResult, CreateBookCommand, GetPagedBooksForModerationQuery, ListedBookDto, UpdateBookCommand } from "../generated";
import { BooksApi } from "../generated/apis/books-api";
import { mapAutocompleteBookPaginatedResultDtoToView } from "../view-models/autocomplete-book";


const booksApi = new BooksApi(requestConfig)

export const createBookCommand = async (
  command: CreateBookCommand
): Promise<number | undefined> => {
  try {
    const response = await booksApi.apiBooksPost(command)
    return response.data
  } catch (error) {
    console.error(error)
    return undefined
  }
}

export const approveBook = async (
  id: number
) => {
  await booksApi.apiBooksIdApprovePut(id)
}

export const updateBook = async (
  query: UpdateBookCommand
) => {
  await booksApi.apiBooksIdPut(query.id!, query)
}

export const deleteBook = async (
  id: number
) => {
  await booksApi.apiBooksIdDelete(id)
}

export const fetchBooksForModeration = async (
  query: GetPagedBooksForModerationQuery
): Promise<BookForModerationDtoPaginatedResult> => {
  const response = await booksApi.apiBooksPagedModeratedPost(query);

  return response.data;
}

export const fetchAutocompleteBooksSkipLoggedInIds = async (
  query: string
): Promise<ListedBookDto[]> => {
  if (!query || query.length < 3) {
    return [];
  }

  try {
    const response = await booksApi.apiBooksPagedPost({
      pageSize: 10,
      pageNumber: 1,
      title: query,
      orderByProperty: "title",
      orderDirection: "asc",
      skipLoggedInUserBooks: true
    })

    return mapAutocompleteBookPaginatedResultDtoToView(response.data);

  } catch (error) {
    console.error(error);
    return [];
  }
}

export const fetchAutocompleteBooksSkipCustomIds = async (
  query: string,
  idsToSkip?: number[]
): Promise<ListedBookDto[]> => {
  if (!query || query.length < 3) {
    return [];
  }

  try {
    const response = await booksApi.apiBooksPagedPost({
      pageSize: 10,
      pageNumber: 1,
      title: query,
      orderByProperty: "title",
      orderDirection: "asc",
      idsToSkip
    })

    return mapAutocompleteBookPaginatedResultDtoToView(response.data);

  } catch (error) {
    console.error(error);
    return [];
  }
}