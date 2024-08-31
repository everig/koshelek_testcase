using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Serilog;

namespace Application.Services.Messages.Queries.GetMessagesByDate
{
    public class GetMessagesByDateQueryHandler : IRequestHandler<GetMessagesByDateQuery, MessageListVm>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public GetMessagesByDateQueryHandler(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<MessageListVm> Handle(GetMessagesByDateQuery query, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetAll();
            var messagesQuery = from message in messages
                           where message.DateLabel <= query.LateDate && message.DateLabel >= query.EarlyDate
                           select message;

            return new MessageListVm { Messages = _mapper.Map<IEnumerable<MessageDto>>(messagesQuery) };
        }
    }
}
