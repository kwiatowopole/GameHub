using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GameHub.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() : base("Default") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<FavGame> FavGames { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}