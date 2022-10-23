namespace ASDNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalCost = c.Int(nullable: false),
                        Customer_Id = c.Int(),
                        Restaurant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Restaurant_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        Category_Id = c.Int(),
                        Restaurant_Id = c.Int(),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.Category_Id)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_Id)
                .ForeignKey("dbo.Carts", t => t.Cart_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Restaurant_Id)
                .Index(t => t.Cart_Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Cart_Id = c.Int(),
                        Customer_Id = c.Int(),
                        Payment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.Cart_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Payments", t => t.Payment_Id)
                .Index(t => t.Cart_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Payment_Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillingName = c.String(nullable: false),
                        BillingEmail = c.String(nullable: false),
                        BillingStreetNum = c.String(nullable: false),
                        BillingStreet = c.String(nullable: false),
                        BillingSuburb = c.String(nullable: false),
                        BillingState = c.String(nullable: false),
                        BillingPostCode = c.String(nullable: false),
                        CreditCardName = c.String(nullable: false),
                        CreditCardNumber = c.String(nullable: false),
                        CreditCardExpiry = c.String(nullable: false),
                        CVV = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Payment_Id", "dbo.Payments");
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Orders", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.Carts", "Restaurant_Id", "dbo.Restaurants");
            DropForeignKey("dbo.Products", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.Products", "Restaurant_Id", "dbo.Restaurants");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.ProductCategories");
            DropForeignKey("dbo.Carts", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Payment_Id" });
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropIndex("dbo.Orders", new[] { "Cart_Id" });
            DropIndex("dbo.Products", new[] { "Cart_Id" });
            DropIndex("dbo.Products", new[] { "Restaurant_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.Carts", new[] { "Restaurant_Id" });
            DropIndex("dbo.Carts", new[] { "Customer_Id" });
            DropTable("dbo.Payments");
            DropTable("dbo.Orders");
            DropTable("dbo.Restaurants");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.Customers");
            DropTable("dbo.Carts");
        }
    }
}
