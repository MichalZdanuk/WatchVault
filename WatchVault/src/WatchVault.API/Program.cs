using WatchVault.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.Services.InitialiseDatabase();
}

app.UseApiServices();

app.UseHttpsRedirection();

app.MapGet("/ping", () =>
{
    return "pong";
});

app.Run();

