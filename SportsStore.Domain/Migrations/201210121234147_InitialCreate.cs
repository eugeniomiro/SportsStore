namespace SportsStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using SportsStore.Domain.Entities;
    using System.Globalization;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);

            Product[] prods = { 
                                new Product { Name = "Kayak",       Description = "A boat for one person",          Category = "Watersports",   Price = 275M },
                                new Product { Name = "Lifejacket",  Description = "Protective and fashionable",     Category = "Watersports",   Price = 48.95M },
                                new Product { Name = "Soccer ball", Description = "FIFA-approved size and weight",  Category = "Soccer",        Price = 19.50M },
                                new Product { Name = "Corner flags", 
                                                                    Description = "Give your playing field that professional touch", 
                                                                                                                    Category = "Soccer",        Price = 34.95M },
                                new Product { Name = "Stadium",     Description = "Flat-packed 35,000-seat stadium", 
                                                                                                                    Category = "Soccer",        Price = 79500M },
                                new Product { Name = "Thinking cap", 
                                                                    Description = "Improve your brain efficiency by 75%", 
                                                                                                                    Category = "Chess",         Price = 16M },
                                new Product { Name = "Unsteady Chair", 
                                                                    Description = "Secretly give your opponent a disadvantage", 
                                                                                                                    Category = "Chess",         Price = 29.95M },
                                new Product { Name = "Human Chess ...", 
                                                                    Description = "A fun game for the whole family", 
                                                                                                                    Category = "Chess",         Price = 75M },
                                new Product { Name = "Bling-bling King", 
                                                                    Description = "Gold-plated, diamond-studded King", 
                                                                                                                    Category = "Chess",         Price = 1200M }
                              };

            NumberFormatInfo numberInfo = (NumberFormatInfo) CultureInfo.CurrentCulture.NumberFormat.Clone();
            numberInfo.CurrencyDecimalSeparator = ".";
            numberInfo.CurrencyGroupSeparator = string.Empty;

            foreach (var p in prods) {
                Sql(string.Format(@"INSERT INTO [dbo].[Products] ([Name], [Description], [Price], [Category]) VALUES ('{0}', '{1}', {2}, '{3}');", p.Name, p.Description, p.Price.ToString("c", numberInfo), p.Category));
            }
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
