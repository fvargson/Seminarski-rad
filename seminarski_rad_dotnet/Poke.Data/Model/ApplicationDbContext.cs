using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Poke.Data.Repository;

namespace Poke.Data.Model;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<PokeCategory>()
            .HasData(
                new PokeCategory() 
                {
                    Id = 1,
                    Name = "Generation"
                },
                new PokeCategory()
                {
                    Id = 2,
                    Name = "Type"
                },
                new PokeCategory()
                {
                    Id = 3,
                    Name = "Other"
                }
            );
        
        List<SubCategory> Generations = new List<SubCategory>();
        int i;
        for(i = 1; i < 9; i++)
        {
            Generations.Add(new SubCategory() { Id = i, Name = "Gen. " + i, PokeCategoryId = 1 });
        }

        modelBuilder
            .Entity<SubCategory>()
            .HasData(
                Generations
            );
        
        var _poke_repo = new PokeRepository();

        List<SubCategory> Types = new List<SubCategory>();

        _poke_repo
            .GetPokemonTypes()
            .ToList()
            .ForEach(
                t => Types
                        .Add(
                            new SubCategory()
                            {
                                Id = i++,
                                Name = t,
                                PokeCategoryId = 2
                            }
                        )
                );
        
        modelBuilder
            .Entity<SubCategory>()
            .HasData(
                Types
            );
        
        modelBuilder
            .Entity<SubCategory>()
            .HasData(
                new SubCategory()
                {
                    Id = i++, //27
                    Name = "Starter",
                    PokeCategoryId = 3
                },
                new SubCategory()
                {
                    Id = i++, //28
                    Name = "Legendary",
                    PokeCategoryId = 3
                },
                new SubCategory()
                {
                    Id = i++, //29
                    Name = "Mythical",
                    PokeCategoryId = 3
                },
                new SubCategory()
                {
                    Id = i++, //30
                    Name = "Baby",
                    PokeCategoryId = 3
                }
            );

        string[] pokes = new string[] 
            {
                "Bulbasaur", "Charmander", "Squirtle", 
                "Chikorita", "Cyndaquil", "Totodile",
                "Treecko", "Torchic", "Mudkip",
                "Turtwig", "Chimchar", "Piplup",
                "Snivy", "Tepig", "Oshawott",
                "Chespin", "Fennekin", "Froakie",
                "Rowlet", "Litten", "Popplio",
                "Grookey", "Scorbunny", "Sobble"
            };
        
        List<PokeProduct> pokeProducts = new List<PokeProduct>();
        for(int j = 1; j <= pokes.Length; j++)
        {
            pokeProducts.Add(
                new PokeProduct() 
                {
                    Id = j,
                    Name = pokes[j-1],
                    Price = 2500,
                    Quantity = 50,
                    PokemonId = _poke_repo.GetPokemonById(pokes[j-1]).Id,
                    InBundle = false
                }
            );
        }

        modelBuilder
            .Entity<PokeProduct>()
            .HasData(
                pokeProducts
            );
        
        List<PokeProductCategory> pokeProductCategory = new List<PokeProductCategory>();
        
        int k = 1;
        for(int j = 1; j <= pokes.Length; j++)
        {
            int gen = _poke_repo.GetPokemonGeneration(pokes[j-1]);

            pokeProductCategory.Add(
                new PokeProductCategory()
                {
                    Id = k++,
                    PokeProductId = j,
                    SubCategoryId = gen
                }
            );

            var pokeType = _poke_repo.GetPokemonType(pokes[j-1]);

            foreach(var typing in pokeType)
            {
                pokeProductCategory.Add(
                    new PokeProductCategory()
                    {
                        Id = k++,
                        PokeProductId = j,
                        SubCategoryId = Types.FirstOrDefault(t => t.Name == typing).Id
                    }
                );
            }

            pokeProductCategory.Add(
                new PokeProductCategory()
                {
                    Id = k++,
                    PokeProductId = j,
                    SubCategoryId = 27
                }
            );
        }

        modelBuilder
            .Entity<PokeProductCategory>()
            .HasData(
                pokeProductCategory
            );
    }

    public DbSet<PokeCategory> PokeCategories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<PokeBundle> PokeBundles { get; set; }
    public DbSet<PokeProduct> PokeProducts { get; set; }
    public DbSet<PokeProductCategory> PokeProductCategories { get; set; }
    public DbSet<PokeBundleProduct> PokeBundleProducts { get; set; }
    public DbSet<PokeInvoice> PokeInvoices { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Avatar> Avatars { get; set; }
}