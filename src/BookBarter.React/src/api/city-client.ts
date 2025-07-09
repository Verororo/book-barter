import { CitiesApi, type CityDto } from "./generated";
import { Configuration } from "./generated/configuration";
import { mapCityPaginatedResultDtoToView } from "./view-models/autocomplete-city";

const cfg = new Configuration({ basePath: `${import.meta.env.VITE_API_BASE_URL}` })
const citiesApi = new CitiesApi(cfg)

export const fetchPagedCities = async (query: string): Promise<CityDto[]> => {
  try {
    const response = await citiesApi.apiCitiesPagedPost({
      pageSize: 10,
      pageNumber: 1,
      orderByProperty: "nameWithCountry",
      orderDirection: "asc",
      query: query,
    })

    return mapCityPaginatedResultDtoToView(response.data);

  } catch (error) {
    console.error(error);
    return [];
  }
}