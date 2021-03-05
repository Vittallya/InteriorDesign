namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommonCostInOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CommonCost", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CommonCost");
        }
    }
}
