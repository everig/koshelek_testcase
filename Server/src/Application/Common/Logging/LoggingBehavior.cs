using MediatR;
using Serilog;

namespace Application.Common.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public LoggingBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken token)
        {
            _logger.Information($"Handling {typeof(TRequest).Name}");
            var response = await next();
            _logger.Information($"Handled {typeof(TResponse).Name}");
            return response;
        }
    }
}
