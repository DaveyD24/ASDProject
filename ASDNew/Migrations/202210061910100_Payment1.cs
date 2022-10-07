namespace ASDNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payment1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payments", "CVV", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "CVV", c => c.Int(nullable: false));
        }
    }
}
