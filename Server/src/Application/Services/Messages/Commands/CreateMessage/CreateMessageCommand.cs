using Domain.Entities;
using MediatR;

namespace Application.Services.Messages.Commands.CreateMessage
{
    public class CreateMessageCommand : IRequest
    {
        public int No { get; set; }
        public DateTime DateLabel { get; set; }
        public string Text { get; set; }
    }
}
