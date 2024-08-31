using Application.Common.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Domain.Entities;
using Serilog;

namespace Application.Services.Messages.Commands.DeleteMessage
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ILogger _logger;

        public DeleteMessageCommandHandler(IMessageRepository messageRepository, ILogger logger) 
        {
            _messageRepository = messageRepository;
            _logger = logger;
        }


        public async Task Handle(DeleteMessageCommand request, CancellationToken cancellation)
        {
            var message = _messageRepository.Get(request.No);
            if (message == null)
            {
                _logger.Information($"Exception {nameof(NotFoundException)} in {request.GetType}");
                throw new NotFoundException(nameof(Message), request.No);
            }
            await _messageRepository.Delete(request.No);
        }
    }
}
