namespace GameHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRemoveGameDescCat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Category", c => c.String());
            DropColumn("dbo.Games", "description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "description", c => c.String());
            DropColumn("dbo.Games", "Category");
        }
    }
}
