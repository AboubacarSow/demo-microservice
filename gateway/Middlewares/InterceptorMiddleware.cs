namespace gateway.Middlewares;

public class InterceptorMiddleware(ILogger<InterceptorMiddleware> logger,RequestDelegate next){

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.Headers.Append("Reference", "api-gateway");

        var present = context.Request.Headers.ContainsKey("Reference"); // should be true
        var header = context.Request.Headers["Reference"]; 
        logger.LogInformation($"Present: {present}, header: {header}");
        await next(context);
    }

}

public class GatewayHeaderHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.Headers.Add("Reference", "api-gateway");

        return await base.SendAsync(request, cancellationToken);
    }
}