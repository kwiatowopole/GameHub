namespace GameHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        gameId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.gameId);
            
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
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userId = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false, maxLength: 30),
                        emailAddress = c.String(),
                    })
                .PrimaryKey(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scores", "userId", "dbo.Users");
            DropForeignKey("dbo.Scores", "gameId", "dbo.Games");
            DropIndex("dbo.Scores", new[] { "gameId" });
            DropIndex("dbo.Scores", new[] { "userId" });
            DropTable("dbo.Users");
            DropTable("dbo.Scores");
            DropTable("dbo.Games");
        }
    }
}
