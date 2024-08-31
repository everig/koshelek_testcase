using Application.Common.Interfaces;
using Application.Services.Messages.Commands.CreateMessage;
using AutoMapper;

namespace TestCase.Models
{
    public class CreateMessageDto : IMapWith<CreateMessageCommand>
    {
        public int No { get; set; }
        public string Text { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateMessageDto, CreateMessageCommand>(); 
        }
    }
}
