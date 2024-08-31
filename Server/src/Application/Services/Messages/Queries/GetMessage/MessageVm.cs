using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.Messages.Queries.GetMessage
{
    public class MessageVm : IMapWith<Message>
    {
        public int No { get; set; }
        public DateTime DateLabel { get; set; }
        public string Text { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Message, MessageVm>();
        }
    }
}
