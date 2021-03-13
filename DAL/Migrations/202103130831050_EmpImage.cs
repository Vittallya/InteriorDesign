namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "ImagePath");
        }
    }
}
