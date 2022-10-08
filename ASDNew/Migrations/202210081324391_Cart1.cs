﻿namespace ASDNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "Quantity");
        }
    }
}
