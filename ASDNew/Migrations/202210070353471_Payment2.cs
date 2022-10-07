namespace ASDNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payment2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "BillingStreetNum", c => c.String(nullable: false));
            AddColumn("dbo.Payments", "BillingStreet", c => c.String(nullable: false));
            DropColumn("dbo.Payments", "BillingAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "BillingAddress", c => c.String(nullable: false));
            DropColumn("dbo.Payments", "BillingStreet");
            DropColumn("dbo.Payments", "BillingStreetNum");
        }
    }
}
