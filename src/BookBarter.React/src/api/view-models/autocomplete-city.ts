import type { CityDto, CityDtoPaginatedResult } from "../generated"

export const mapCityPaginatedResultDtoToView = (dto: CityDtoPaginatedResult): CityDto[] => (
  dto.items ?? []
)