import { BooksApi } from "./generated/apis/books-api";
import { Configuration } from "./generated/configuration";
import { mapAutocompleteBookPaginatedDtoToView, type AutocompleteBookItem } from "./view-models/autocomplete-book-paginated-result";

const cfg = new Configuration({ basePath: `${import.meta.env.VITE_API_BASE_URL}` })
const booksApi = new BooksApi(cfg)

export const fetchAutocompleteBooksPaginated = async (title: string): Promise<AutocompleteBookItem[]> => {
  if (title.length < 3) {
    return [];
  }

  try {
    const response = await booksApi.apiBooksPagedPost({
      pageSize: 10,
      pageNumber: 1,
      title: title,
      orderByProperty: "title",
      orderDirection: "asc"
    })

    return mapAutocompleteBookPaginatedDtoToView(response.data);

  } catch (error) {
    console.error("Failed to fetch book suggestions:", error);
    return [];
  }
}