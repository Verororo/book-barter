import { CitiesApi, type CityDto } from "../generated";
import { requestConfig } from "./common"
import { mapCityPaginatedResultDtoToView } from "../view-models/autocomplete-city";

const citiesApi = new CitiesApi(requestConfig)

export const fetchPagedCities = async (
  query: string
): Promise<CityDto[]> => {
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
    throw error;
  }
}