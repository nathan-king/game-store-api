using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    private const string GetGameEndpoint = "GetGame";

    private static readonly List<GameDto> Games =
    [
        new(1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
        new(2, "Legend of Zelda", "Fantasy", 14.99M, new DateOnly(1995, 8, 10)),
        new(3, "Mario Kart", "Racing", 20.99M, new DateOnly(2001, 11, 20))
    ];


    public static void MapGamesEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("/games");

        // GET /games
        group.MapGet("/", () => Games);

        // Get /games/:id
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = Games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndpoint);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                Games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            Games.Add(game);
            return Results.CreatedAtRoute(GetGameEndpoint, new { id = game.Id }, game);
        });

        // PUT /games/id + body
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            int index = Games.FindIndex(game => game.Id == id);

            if (index == -1) return Results.NotFound();

            Games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // DELETE /games/id
        group.MapDelete("/{id}", (int id) =>
        {
            int removed = Games.RemoveAll(game => game.Id == id);
            return removed == 0 ? Results.NotFound() : Results.NoContent();
        });
    }
}