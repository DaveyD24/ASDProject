namespace ASDNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
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
                        Customer_Id = c.Int(),
                        Payment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Payments", t => t.Payment_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Payment_Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillingName = c.String(),
                        BillingAddress = c.String(),
                        BillingSuburb = c.String(),
                        BillingState = c.String(),
                        BillingPostCode = c.String(),
                        CreditCardNumber = c.String(),
                        CreditCardExpiry = c.String(),
                        CVV = c.Int(nullable: false),
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
                        Category_Id = c.Int(),
                        Restaurant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.Category_Id)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Restaurant_Id);
            
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
                        Description = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Restaurant_Id", "dbo.Restaurants");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.ProductCategories");
            DropForeignKey("dbo.Orders", "Payment_Id", "dbo.Payments");
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Products", new[] { "Restaurant_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.Orders", new[] { "Payment_Id" });
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropTable("dbo.Restaurants");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.Payments");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
