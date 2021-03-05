namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "Service_Id", "dbo.Services");
            DropIndex("dbo.OrderDetails", new[] { "Service_Id" });
            DropColumn("dbo.OrderDetails", "Service_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Service_Id", c => c.Int());
            CreateIndex("dbo.OrderDetails", "Service_Id");
            AddForeignKey("dbo.OrderDetails", "Service_Id", "dbo.Services", "Id");
        }
    }
}
