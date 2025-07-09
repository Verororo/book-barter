
using AutoMapper;
using BookBarter.Application.Cities.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Cities;

public class CityProfile : Profile
{
    public CityProfile() 
    {
        CreateMap<City, CityDto>()
            .ForMember(x => x.NameWithCountry, o => o.MapFrom(x => x.Name + ", " + x.CountryName));
    }
}
