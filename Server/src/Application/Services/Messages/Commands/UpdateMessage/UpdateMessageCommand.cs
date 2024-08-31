using MediatR;

namespace Application.Services.Messages.Commands.UpdateMessage
{
    public class UpdateMessageCommand : IRequest
    {
        public int No {  get; set; }
        public DateTime DateLabel { get; set; }
        public string Text { get; set; }
    }
}
