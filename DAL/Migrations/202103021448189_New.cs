namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Email", c => c.String());
            DropColumn("dbo.Users", "Login");
            DropColumn("dbo.Users", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Image", c => c.Binary());
            AddColumn("dbo.Users", "Login", c => c.String());
            DropColumn("dbo.Users", "Email");
        }
    }
}
