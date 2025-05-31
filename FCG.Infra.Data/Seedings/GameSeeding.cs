using FCG.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Data.Seedings
{
    public static class GameSeeding
    {
        public static async Task SeedAsync(DbContext context)
        {
            Console.WriteLine("Starting game seed...");
            var gamesToSeed = new List<Game>()
            {
                new("The Legend of Zelda: Breath of the Wild", 59.99m, "Um jogo de aventura em mundo aberto onde você explora o reino de Hyrule.", "Aventura"),
                new("Super Mario Odyssey", 49.99m, "Uma aventura 3D com Mario em diversos mundos para resgatar a Princesa Peach.", "Plataforma"),
                new("Minecraft", 26.95m, "Um jogo de construção e sobrevivência em um mundo gerado aleatoriamente.", "Sandbox"),
                new("The Witcher 3: Wild Hunt", 39.99m, "Um RPG de ação em mundo aberto onde você joga como Geralt de Rivia em busca de sua filha adotiva.", "RPG"),
                new("Dark Souls III", 29.99m, "Um RPG de ação desafiador com combate intenso e exploração em um mundo sombrio.", "Ação/RPG"),
                new("God of War", 39.99m, "Uma reinvenção da série God of War, focada na relação entre Kratos e seu filho Atreus.", "Ação/Aventura"),
                new("Hollow Knight", 14.99m, "Um jogo de plataforma e ação em um mundo subterrâneo cheio de segredos e desafios.", "Metroidvania"),
            };

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
