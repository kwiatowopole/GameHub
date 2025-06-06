namespace GameHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPasswordToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "password", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "password");
        }
    }
}
