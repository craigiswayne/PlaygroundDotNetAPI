using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using PlaygroundDotNetAPI.Models;

namespace PlaygroundDotNetAPI.Data;

public class MyDbContextSqLite(DbContextOptions<MyDbContextSqLite> options) : DbContext(options)
{
    public DbSet<Pokemon> Pokedex { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pokemon>().HasData(
            new Pokemon { Id = 1, Name = "Bulbasaur" },
            new Pokemon { Id = 2, Name = "Ivysaur" },
            new Pokemon { Id = 3, Name = "Venusaur" },
            new Pokemon { Id = 4, Name = "Charmander" },
            new Pokemon { Id = 5, Name = "Charmeleon" },
            new Pokemon { Id = 6, Name = "Charizard" },
            new Pokemon { Id = 7, Name = "Squirtle" },
            new Pokemon { Id = 8, Name = "Wartortle" },
            new Pokemon { Id = 9, Name = "Blastoise" }
        );
    }

}