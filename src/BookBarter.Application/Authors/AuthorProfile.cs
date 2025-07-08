using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookBarter.Application.Common.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Authors;

public class AuthorProfile : Profile
{
    public AuthorProfile() 
    {
        CreateMap<Author, AuthorDto>();
    }
}
