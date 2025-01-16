using PlaygroundDotNetAPI.Data;
using PlaygroundDotNetAPI.Models;

namespace PlaygroundDotNetAPI.Services;

public interface IPokedexService
{
    IQueryable<Pokemon> List();
    ValueTask<Pokemon?> Get(int pokemonId);
}

public class PokedexService(MyDbContextSqLite dbContext) : IPokedexService
{
    public IQueryable<Pokemon> List()
    {
        return dbContext.Pokedex.OrderBy(columns => columns.Id).Skip(0).Take(10);
    }

    public ValueTask<Pokemon?> Get(int pokemonId)
    {
        return dbContext.Pokedex.FindAsync(pokemonId);
    }
}