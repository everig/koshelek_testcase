using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Serilog;

namespace Application.Services.Messages.Queries.GetMessage
{
    public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, MessageVm>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetMessageQueryHandler(IMessageRepository messageRepository, IMapper mapper, ILogger logger)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MessageVm> Handle(GetMessageQuery query, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.Get(query.No);
            if (message == null)
            {
                _logger.Information($"Exception {nameof(NotFoundException)} in {query.GetType}");
                throw new NotFoundException(nameof(Message), query.No);
            }
            return _mapper.Map<MessageVm>(message);
        }
    }
}
