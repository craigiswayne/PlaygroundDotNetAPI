using PlaygroundDotNetAPI.Data;
using PlaygroundDotNetAPI.Models;

namespace PlaygroundDotNetAPI.Services;

public interface IPokedexService
{
    IQueryable<Pokemon> List();
}

public class PokedexService(MyDbContextSqLite dbContext) : IPokedexService
{
    private readonly MyDbContextSqLite _dbContext = dbContext;

    public IQueryable<Pokemon> List()
    {
        return _dbContext.Pokedex.OrderBy(columns => columns.Id).Skip(0).Take(5);
    }
}