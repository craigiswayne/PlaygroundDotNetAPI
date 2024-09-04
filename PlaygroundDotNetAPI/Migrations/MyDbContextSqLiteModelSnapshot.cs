﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlaygroundDotNetAPI.Data;

#nullable disable

namespace PlaygroundDotNetAPI.Migrations
{
    [DbContext(typeof(MyDbContextSqLite))]
    partial class MyDbContextSqLiteModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("PlaygroundDotNetAPI.Models.Pokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Pokedex");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Bulbasaur"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ivysaur"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Venusaur"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Charmander"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Charmeleon"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Charizard"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Squirtle"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Wartortle"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Blastoise"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
