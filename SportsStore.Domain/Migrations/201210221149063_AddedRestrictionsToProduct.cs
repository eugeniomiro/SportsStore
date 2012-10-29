namespace SportsStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRestrictionsToProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Products", "Category", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Category", c => c.String(maxLength: 50));
            AlterColumn("dbo.Products", "Description", c => c.String(maxLength: 500));
        }
    }
}
