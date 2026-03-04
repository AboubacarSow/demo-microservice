using Microsoft.AspNetCore.Http.HttpResults;
using shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseMiddleware<RequestGuardMiddleware>();
var users = new List<User>()
{
    new(Guid.NewGuid(),"John Doe",25),
    new(Guid.NewGuid(),"Pierre Florentores",35),
    new(Guid.NewGuid(),"Micheal DuBois",45),
    new(Guid.NewGuid(),"Kalidou Koulibaly",29),
};


app.MapGet("api/users", () =>
{
    return Results.Ok(users);
}).WithName("Get");



app.Run();

record User(Guid Id,string Name, int Age);
