using GameStore.Api.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapGamesEndpoints();

app.Run();