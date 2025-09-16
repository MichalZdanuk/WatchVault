using WatchVault.API.Endpoints;
using WatchVault.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.Services.InitialiseDatabase();
}

app.MapEndpoints();

app.UseApiServices()
 .UseSwaggerDocumentation();

app.UseHttpsRedirection();

app.Run();

