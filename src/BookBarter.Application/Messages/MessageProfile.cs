
using AutoMapper;
using BookBarter.Application.Messages.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Messages;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.UserName))
            .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.Receiver.UserName));
    }
}