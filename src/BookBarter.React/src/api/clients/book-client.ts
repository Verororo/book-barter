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
  try {
    await booksApi.apiBooksIdApprovePut(id)
  } catch (error) {
    console.error(error)
    throw error
  }
}

export const updateBook = async (
  query: UpdateBookCommand
) => {
  try {
    await booksApi.apiBooksIdPut(query.id!, query)
  } catch (error) {
    console.error(error)
    throw error
  }
}

export const deleteBook = async (
  id: number
) => {
  try {
    booksApi.apiBooksIdDelete(id)
  } catch (error) {
    console.error(error)
    throw error
  }
}

export const fetchBooksForModeration = async (
  query: GetPagedBooksForModerationQuery
): Promise<BookForModerationDtoPaginatedResult> => {
  try {
    const response = await booksApi.apiBooksPagedModeratedPost(query);
    return response.data;
  } catch (error) {
    console.error(error)
    throw error
  }
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
    throw error;
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
    throw error;
  }
}