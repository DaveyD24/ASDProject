namespace ASDNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BillingName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "BillingEmail", c => c.String());
            AddColumn("dbo.Payments", "CreditCardName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "CreditCardName");
            DropColumn("dbo.Payments", "BillingEmail");
        }
    }
}
