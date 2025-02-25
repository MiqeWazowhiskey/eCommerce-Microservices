using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest,TResponse>(ILogger<LoggingBehavior<TRequest,TResponse>> logger)
    : IPipelineBehavior<TRequest,TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request={request} - Response={response} - RequestData={requestData}"
            , typeof(TRequest).Name, typeof(TResponse).Name, request);
        var timer = new Stopwatch();
        timer.Start();
        
        var response = await next();
        
        timer.Stop();
        var timeTook = timer.ElapsedMilliseconds;
        logger.LogWarning("[PERFORMANCE] Handle request={request} - Response={response} - TimeTook={timeTook}ms"
            , typeof(TRequest).Name, typeof(TResponse).Name, timeTook);
        
        logger.LogWarning("[END] Handle request={request} - Response={response} - ResponseData={responseData}"
            , typeof(TRequest).Name, typeof(TResponse).Name, response);
        return response;
    }
}
