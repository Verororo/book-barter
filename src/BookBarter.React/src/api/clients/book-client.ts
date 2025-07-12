import { requestConfig } from "./common";
import type { CreateBookCommand, ListedBookDto } from "../generated";
import { BooksApi } from "../generated/apis/books-api";
import { mapAutocompleteBookPaginatedDtoToView } from "../view-models/autocomplete-book";


const booksApi = new BooksApi(requestConfig)

export const createBookCommand = async (command: CreateBookCommand): Promise<number | undefined> => {
  try {
    const response = await booksApi.apiBooksPost(command)
    return response.data
  } 
  catch (error) {
    console.error(error)
    return undefined
  }
}

export const fetchAutocompleteBooksPaginated = async (query: string): Promise<ListedBookDto[]> => {
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
      skipLoggedInUserBooks: false
    })

    return mapAutocompleteBookPaginatedDtoToView(response.data);

  } 
  catch (error) {
    console.error(error);
    return [];
  }
}

export const fetchAutocompleteBooksPaginatedSkipRelated = async (query: string): Promise<ListedBookDto[]> => {
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

    return mapAutocompleteBookPaginatedDtoToView(response.data);

  } 
  catch (error) {
    console.error(error);
    return [];
  }
}