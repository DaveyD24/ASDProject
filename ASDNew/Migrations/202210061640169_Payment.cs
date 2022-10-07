namespace ASDNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payments", "BillingName", c => c.String(nullable: false));
            AlterColumn("dbo.Payments", "BillingEmail", c => c.String(nullable: false));
            AlterColumn("dbo.Payments", "BillingAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Payments", "BillingSuburb", c => c.String(nullable: false));
            AlterColumn("dbo.Payments", "BillingState", c => c.String(nullable: false));
            AlterColumn("dbo.Payments", "BillingPostCode", c => c.String(nullable: false));
            AlterColumn("dbo.Payments", "CreditCardName", c => c.String(nullable: false));
            AlterColumn("dbo.Payments", "CreditCardNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Payments", "CreditCardExpiry", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "CreditCardExpiry", c => c.String());
            AlterColumn("dbo.Payments", "CreditCardNumber", c => c.String());
            AlterColumn("dbo.Payments", "CreditCardName", c => c.String());
            AlterColumn("dbo.Payments", "BillingPostCode", c => c.String());
            AlterColumn("dbo.Payments", "BillingState", c => c.String());
            AlterColumn("dbo.Payments", "BillingSuburb", c => c.String());
            AlterColumn("dbo.Payments", "BillingAddress", c => c.String());
            AlterColumn("dbo.Payments", "BillingEmail", c => c.String());
            AlterColumn("dbo.Payments", "BillingName", c => c.String());
        }
    }
}
