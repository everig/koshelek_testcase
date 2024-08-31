using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Serilog;

namespace Application.Services.Messages.Commands.UpdateMessage
{
    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ILogger _logger;
        public UpdateMessageCommandHandler(IMessageRepository messageRepository, ILogger logger) 
        { 
            _messageRepository = messageRepository;
            _logger = logger;
        }

        public async Task Handle(UpdateMessageCommand request, CancellationToken cancellation) 
        {
            var message = await _messageRepository.Get(request.No);
            if (message == null)
            {
                _logger.Information($"Exception {nameof(NotFoundException)} in {request.GetType}");
                throw new NotFoundException(nameof(Message), request.No);
            }
            await _messageRepository.Update(message);
        }
    }
}
