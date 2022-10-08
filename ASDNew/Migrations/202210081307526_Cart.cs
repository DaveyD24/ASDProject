namespace ASDNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Carts", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "Quantity", c => c.Int(nullable: false));
        }
    }
}
