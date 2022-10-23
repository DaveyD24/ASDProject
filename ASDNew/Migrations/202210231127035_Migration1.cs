namespace ASDNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurants", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurants", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurants", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "Password", c => c.String());
            AlterColumn("dbo.Restaurants", "Email", c => c.String());
            AlterColumn("dbo.Restaurants", "Description", c => c.String());
            AlterColumn("dbo.Restaurants", "Name", c => c.String());
        }
    }
}
