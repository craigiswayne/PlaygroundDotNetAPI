using PlaygroundDotNetAPI.Data;
using PlaygroundDotNetAPI.Models;

namespace PlaygroundDotNetAPI.Services;

public interface IPokedexService
{
    IQueryable<Pokemon> List();
}

public class PokedexService(MyDbContextSqLite dbContext) : IPokedexService
{
    public IQueryable<Pokemon> List()
    {
        return dbContext.Pokedex.OrderBy(columns => columns.Id).Skip(0).Take(10);
    }
}