using AutoMapper;
using BookBarter.Application.Publishers.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Publishers;

public class PublisherProfile : Profile
{
    public PublisherProfile() 
    {
        CreateMap<Publisher, PublisherDto>();
    }
}
