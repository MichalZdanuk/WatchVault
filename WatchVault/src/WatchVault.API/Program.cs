using Keycloak.AuthServices.Authentication;
using WatchVault.API.Endpoints;
using WatchVault.API.Options;
using WatchVault.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiOptions(builder.Configuration);
builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddRateLimiting();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseRateLimiting();

if (app.Environment.IsDevelopment())
{
    app.Services.InitialiseDatabase();
}

app.MapEndpoints();

app.UseApiServices()
 .UseSwaggerDocumentation();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

