using Microsoft.EntityFrameworkCore;
using PlaygroundDotNetAPI.Models;

namespace PlaygroundDotNetAPI.Data;

public class MyDbContextSqLite : DbContext
{
    public MyDbContextSqLite(DbContextOptions<MyDbContextSqLite> options) : base(options)
    {
    }

    public DbSet<Pokemon> Pokemon { get; set; }

}