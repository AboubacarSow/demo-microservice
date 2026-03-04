using gateway.Middlewares;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
      .SetBasePath(builder.Environment.ContentRootPath)
      .AddJsonFile("ocelot.json", optional:false, reloadOnChange:true) ;

builder.Services.AddOcelot(builder.Configuration)
    .AddDelegatingHandler<GatewayHeaderHandler>(false);
// Add services  the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(configurePolicy =>
    {
        configurePolicy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
    });
});


//builder.Services.AddTransient<InterceptorMiddleware>();

var app = builder.Build();
//app.UseMiddleware<InterceptorMiddleware>();
app.UseCors();

app.UseHttpsRedirection();
app.UseOcelot().Wait();

app.Run();


