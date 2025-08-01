﻿using AutoMapper;
using BookBarter.Application.Common.Responses;
using BookBarter.Application.Publishers.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Publishers;

public class PublisherProfile : Profile
{
    public PublisherProfile() 
    {
        CreateMap<Publisher, PublisherDto>();
        CreateMap<Publisher, PublisherForModerationDto>();
    }
}
