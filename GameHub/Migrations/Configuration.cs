namespace GameHub.Migrations
{
    using GameHub.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GameHub.Models.AppDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


        protected override void Seed(GameHub.Models.AppDBContext context)
        {
            // 1. Dodaj kategorie (Categories)
            context.Categories.AddOrUpdate(c => c.CategoryId,
                new Category { CategoryId = 1, Name = "Precision" },
                new Category { CategoryId = 2, Name = "Memory" },
                new Category { CategoryId = 3, Name = "Reflex" }
            );
            context.SaveChanges();

            // Pobierz kategorie z bazy, aby mieć ich ID
            var precisionCategory = context.Categories.FirstOrDefault(c => c.Name == "Precision");
            var memoryCategory = context.Categories.FirstOrDefault(c => c.Name == "Memory");
            var reflexCategory = context.Categories.FirstOrDefault(c => c.Name == "Reflex");

            // 2. Dodaj gry (Games) z przypisaniem CategoryId (klucz obcy)
            context.Games.AddOrUpdate(g => g.name,
                new Game { name = "AimTrainer", CategoryId = precisionCategory.CategoryId },
                new Game { name = "Refleks", CategoryId = reflexCategory.CategoryId },
                new Game { name = "SimonSays", CategoryId = memoryCategory.CategoryId },
                new Game { name = "GridExperiment", CategoryId = memoryCategory.CategoryId },
                new Game { name = "HoverPointer", CategoryId = precisionCategory.CategoryId }
            );
            context.SaveChanges();

            // 3. Dodaj użytkowników (Users)
            context.Users.AddOrUpdate(u => u.emailAddress,
                new User { username = "admin", emailAddress = "admin@example.com", password = "admin123", isAdmin = true },
                new User { username = "player1", emailAddress = "player1@example.com", password = "player123", isAdmin = false }
            );
            context.SaveChanges();

            // Pobierz dodanych użytkowników i gry
            var adminUser = context.Users.FirstOrDefault(u => u.emailAddress == "admin@example.com");
            var playerUser = context.Users.FirstOrDefault(u => u.emailAddress == "player1@example.com");

            var aimTrainerGame = context.Games.FirstOrDefault(g => g.name == "AimTrainer");
            var refleksGame = context.Games.FirstOrDefault(g => g.name == "Refleks");
            var simonSaysGame = context.Games.FirstOrDefault(g => g.name == "SimonSays");
            var gridExperimentGame = context.Games.FirstOrDefault(g => g.name == "GridExperiment");
            var hoverPointerGame = context.Games.FirstOrDefault(g => g.name == "HoverPointer");

            // 4. Dodaj wyniki (Scores)
            if (adminUser != null && aimTrainerGame != null)
            {
                context.Scores.AddOrUpdate(s => s.scoreId,
                    new Score { userId = adminUser.userId, gameId = aimTrainerGame.gameId, points = 1500, date = DateTime.Now }
                );
            }

            if (playerUser != null && refleksGame != null)
            {
                context.Scores.AddOrUpdate(s => s.scoreId,
                    new Score { userId = playerUser.userId, gameId = refleksGame.gameId, points = 1200, date = DateTime.Now }
                );
            }
            context.SaveChanges();

            // 5. Dodaj ulubione gry (FavGames)
            context.FavGames.AddOrUpdate(f => f.Id,
                new FavGame { Id = 1, UserId = adminUser.userId, GameId = aimTrainerGame.gameId },
                new FavGame { Id = 2, UserId = playerUser.userId, GameId = hoverPointerGame.gameId }
            );
            context.SaveChanges();
        }
    }
}
