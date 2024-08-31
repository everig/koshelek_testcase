using MediatR;

namespace Application.Services.Messages.Queries.GetMessage
{
    public class GetMessageQuery : IRequest<MessageVm>
    {
        public int No { get; set; }
    }
}
