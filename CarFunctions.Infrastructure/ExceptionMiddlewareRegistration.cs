using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;

namespace CarFunctions.Infrastructure
{
    public static class ExceptionMiddlewareRegistration
    {
        public static void UseExceptionHandling(this IFunctionsWorkerApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
