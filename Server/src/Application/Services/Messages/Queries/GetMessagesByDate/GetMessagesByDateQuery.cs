using MediatR;

namespace Application.Services.Messages.Queries.GetMessagesByDate
{
    public class GetMessagesByDateQuery : IRequest<MessageListVm>
    {
        public DateTime EarlyDate { get; set; }
        public DateTime LateDate { get; set; }
    }
}
