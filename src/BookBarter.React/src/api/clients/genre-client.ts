import { requestConfig } from './common';
import { GenresApi, type GenreDto } from '../generated';

const genresApi = new GenresApi(requestConfig);

export const fetchPagedGenres = async (query: string): Promise<GenreDto[]> => {
  const response = await genresApi.apiGenresPagedPost({
    pageSize: 10,
    pageNumber: 1,
    orderByProperty: 'name',
    orderDirection: 'asc',
    query: query,
  });

  return response.data.items ?? [];
};
