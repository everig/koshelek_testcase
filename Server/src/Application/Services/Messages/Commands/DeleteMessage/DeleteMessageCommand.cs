using MediatR;

namespace Application.Services.Messages.Commands.DeleteMessage
{
    public class DeleteMessageCommand : IRequest
    {
        public int No {  get; set; }
    }
}
