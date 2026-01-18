using GameStore.Api.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

WebApplication app = builder.Build();

app.MapGamesEndpoints();

app.Run();