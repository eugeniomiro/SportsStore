using System.Data.Entity.Migrations;
using System.Globalization;

namespace SportsStore.DataAccess.EntityFramework.Migrations
{
    using Domain.Entities;

    public partial class SportsStoreV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                {
                    ProductID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Description = c.String(nullable: false, maxLength: 500),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Category = c.String(nullable: false, maxLength: 50),
                    ImageData = c.Binary(),
                    ImageMimeType = c.String(),
                })
                .PrimaryKey(t => t.ProductID);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

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

            var numberInfo = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
            numberInfo.CurrencyDecimalSeparator = ".";
            numberInfo.CurrencyGroupSeparator = string.Empty;

            foreach (var p in prods)
            {
                Sql(string.Format(@"INSERT INTO [dbo].[Products] ([Name], [Description], [Price], [Category]) VALUES ('{0}', '{1}', {2}, '{3}');", p.Name, p.Description, p.Price.ToString("c", numberInfo), p.Category));
            }
        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Products");
        }
    }
}
