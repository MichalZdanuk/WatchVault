using Microsoft.Extensions.Logging;

namespace WatchVault.Shared.Behaviors;
public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request={request} - response={response} - requestData={requestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var response = await next();

        logger.LogInformation("[END] Handled {request} with {response}",
            typeof(TRequest).Name, typeof(TResponse).Name);

        return response;
    }
}
