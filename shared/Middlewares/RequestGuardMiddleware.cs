using Microsoft.AspNetCore.Http;

namespace shared.Middlewares;

public class RequestGuardMiddleware (RequestDelegate next){

    public async Task InvokeAsync(HttpContext context)
    {
       if(!context.Request.Headers.TryGetValue("Reference", out var value) 
        || value != "api-gateway")
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync(
                $"Forbidden Request");
            return;
        }

        await next(context);

    }

}