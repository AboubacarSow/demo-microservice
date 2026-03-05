using System.IdentityModel.Tokens.Jwt;
using System.Text;
using gateway.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
      .SetBasePath(builder.Environment.ContentRootPath)
      .AddJsonFile("ocelot.json", optional:false, reloadOnChange:true) ;

builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(op =>
    {
        op.WithDictionaryHandle();
    })
    .AddDelegatingHandler<GatewayHeaderHandler>(false);
    

builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer("Bearer",options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            var authentication = builder.Configuration.GetRequiredSection("Authentication");
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = authentication["ValidIssuer"],
                ValidAudience = authentication["ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(authentication["Key"]!)),
                NameClaimType = JwtRegisteredClaimNames.Sub,


            };


        });

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
app.UseAuthentication();
app.UseAuthorization();
app.UseOcelot().Wait();

app.Run();


