using Application.Services.Messages.Commands.CreateMessage;
using Application.Services.Messages.Queries.GetMessage;
using Application.Services.Messages.Queries.GetMessagesByDate;
using AutoMapper;
using Domain.Entities;
using System.Data;
using System.Data.Common;

namespace Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<DbDataReader, Message>()
                .ForMember(dest => dest.No, opt => opt.MapFrom(src => src.GetValue(0)))
                .ForMember(dest => dest.DateLabel, opt => opt.MapFrom(src => src.GetValue(1)))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.GetValue(2)));
            CreateMap<Message, MessageDto>();
            CreateMap<CreateMessageCommand, MessageVm>();
        }
    }
}
