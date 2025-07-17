
using AutoMapper;
using BookBarter.Application.Cities.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Cities;

public class CityProfile : Profile
{
    public CityProfile() 
    {
        CreateMap<City, CityDto>()
            .ForMember(dest => dest.NameWithCountry, opt => opt.MapFrom(src => 
                src.Name + ", " + src.CountryName));
    }
}
