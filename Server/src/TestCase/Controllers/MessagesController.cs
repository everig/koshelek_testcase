using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Application.Services.Messages.Queries.GetMessagesByDate;
using MediatR;
using TestCase.Models;
using Application.Services.Messages.Commands.CreateMessage;
using Application.Services.Messages.Queries.GetMessage;
using Microsoft.AspNetCore.SignalR;
using TestCase.SignalR;

namespace TestCase.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {

        private readonly Serilog.ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IHubContext<MessageHub, ITypedHubClient> _hub;

        public MessagesController(Serilog.ILogger logger, IMapper mapper, IMediator mediator, IHubContext<MessageHub, ITypedHubClient> hub)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
            _hub = hub;
        }

        /// <summary>
        /// Get list of messages betwen dates
        /// </summary>
        /// <remarks>
        /// Request:
        /// Get /Messages?earlyDate= lateDate=
        /// </remarks>>
        /// <param name="earlyDate"></param>
        /// <param name="lateDate"></param>
        /// <returns>MessageListVm</returns>
        [HttpGet]
        public async  Task<ActionResult<MessageListVm>> GetMessages(DateTime earlyDate, DateTime lateDate)
        {
            var query = new GetMessagesByDateQuery { EarlyDate = earlyDate, LateDate = lateDate };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Create message
        /// </summary>
        /// <remarks>
        /// Request:
        /// POST /Messages
        /// Request body:
        /// {
        ///     No: "12",
        ///     Text: "message text"
        /// }
        /// </remarks>>
        /// <param name="createMessage"></param>
        /// <returns>MessageListVm</returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateMessageDto createMessage)
        {
            var command = _mapper.Map<CreateMessageCommand>(createMessage);
            command.DateLabel = DateTime.UtcNow;
            await _mediator.Send(command);
            var message = _mapper.Map<MessageVm>(command);

            await _hub.Clients.All.Recieve(message.No, message.DateLabel, message.Text);
            _logger.Information($"Sended message: {{{message.No}, {message.DateLabel}, {message.Text}}} from hub");
            return Ok();
        }
    }
}
