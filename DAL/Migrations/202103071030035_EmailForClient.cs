namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailForClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Email", c => c.String());
            DropColumn("dbo.Clients", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Phone", c => c.String());
            DropColumn("dbo.Clients", "Email");
        }
    }
}
