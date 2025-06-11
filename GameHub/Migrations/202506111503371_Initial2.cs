namespace GameHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        gameId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.gameId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.FavGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userId = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false, maxLength: 30),
                        emailAddress = c.String(),
                        password = c.String(nullable: false, maxLength: 20),
                        isAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.userId);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        scoreId = c.Int(nullable: false, identity: true),
                        points = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        userId = c.Int(nullable: false),
                        gameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.scoreId)
                .ForeignKey("dbo.Games", t => t.gameId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId)
                .Index(t => t.gameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scores", "userId", "dbo.Users");
            DropForeignKey("dbo.Scores", "gameId", "dbo.Games");
            DropForeignKey("dbo.FavGames", "UserId", "dbo.Users");
            DropForeignKey("dbo.FavGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Scores", new[] { "gameId" });
            DropIndex("dbo.Scores", new[] { "userId" });
            DropIndex("dbo.FavGames", new[] { "GameId" });
            DropIndex("dbo.FavGames", new[] { "UserId" });
            DropIndex("dbo.Games", new[] { "CategoryId" });
            DropTable("dbo.Scores");
            DropTable("dbo.Users");
            DropTable("dbo.FavGames");
            DropTable("dbo.Categories");
            DropTable("dbo.Games");
        }
    }
}
