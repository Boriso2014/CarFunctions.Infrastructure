using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace CarFunctions.Infrastructure
{
    internal sealed class ExceptionMiddleware : IFunctionsWorkerMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionMiddleware(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
        }

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (AggregateException aex)
            {
                _logger.LogError("Unexpected Error in {EntryPoint}", context.FunctionDefinition.EntryPoint);
                foreach (var ex in aex.InnerExceptions)
                {
                    _logger.LogError("Request failed:{NewLine}{Message}", Environment.NewLine, ex.Message);
                }
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected Error in {EntryPoint}: {Message}", context.FunctionDefinition.EntryPoint, ex.Message);
                throw;
            }
        }
    }
}