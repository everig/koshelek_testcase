using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Serilog;

namespace Application.Services.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand>
    {
        private readonly IMessageRepository _messageRepository;

        public CreateMessageCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Message
            {
                No = request.No,
                DateLabel = request.DateLabel,
                Text = request.Text
            };
            await _messageRepository.Insert(message);
        }
    }
}
