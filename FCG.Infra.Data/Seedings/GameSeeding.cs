using Bogus;
using FCG.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Data.Seedings
{
    public static class GameSeeding
    {
        public static async Task SeedAsync(DbContext context)
        {
            Console.WriteLine("Starting game seed...");
            var faker = new Faker<Game>()
                .RuleFor(g => g.Description, f => f.Lorem.Paragraph(2))
                .RuleFor(g => g.Price, f => decimal.Parse(f.Commerce.Price(1, 1000))) 
                .RuleFor(g => g.Title, f => f.Commerce.ProductName())
                .RuleFor(g => g.Genre, f => f.Lorem.Word());

            var gamesToSeed = faker.Generate(4);

            var exists = await context.Set<Game>()
                .AnyAsync(g => g.Title == gamesToSeed[0].Title);

            if (!exists)
            {
                await context.Set<Game>().AddRangeAsync(gamesToSeed);
                await context.SaveChangesAsync();
            }
            Console.WriteLine("Game seed completed!");
        }
    }
}
