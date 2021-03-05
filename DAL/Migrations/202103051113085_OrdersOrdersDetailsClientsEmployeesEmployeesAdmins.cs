namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdersOrdersDetailsClientsEmployeesEmployeesAdmins : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Phone = c.String(),
                        Name = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeAdmins",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Login = c.String(),
                        Password = c.String(),
                        AccessCode = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Special = c.String(),
                        IsShow = c.Boolean(nullable: false),
                        StartWoriking = c.DateTime(nullable: false),
                        Salary = c.Double(nullable: false),
                        WorkingStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        StyleId = c.Int(),
                        Area = c.Double(nullable: false),
                        FloorsHeight = c.Double(nullable: false),
                        HouseType = c.Int(nullable: false),
                        RoomsCount = c.Int(nullable: false),
                        IsWallAlignment = c.Boolean(nullable: false),
                        Service_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Id)
                .ForeignKey("dbo.Services", t => t.Service_Id)
                .ForeignKey("dbo.Styles", t => t.StyleId)
                .Index(t => t.Id)
                .Index(t => t.StyleId)
                .Index(t => t.Service_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        StartWorkingDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.ServiceId);
            
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Role = c.Int(nullable: false),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.OrderDetails", "StyleId", "dbo.Styles");
            DropForeignKey("dbo.OrderDetails", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.OrderDetails", "Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.EmployeeAdmins", "Id", "dbo.Employees");
            DropIndex("dbo.Orders", new[] { "ServiceId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropIndex("dbo.OrderDetails", new[] { "Service_Id" });
            DropIndex("dbo.OrderDetails", new[] { "StyleId" });
            DropIndex("dbo.OrderDetails", new[] { "Id" });
            DropIndex("dbo.EmployeeAdmins", new[] { "Id" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeAdmins");
            DropTable("dbo.Clients");
        }
    }
}
