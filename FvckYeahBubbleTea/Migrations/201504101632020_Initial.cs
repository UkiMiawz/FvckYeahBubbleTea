namespace FvckYeahBubbleTea.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaseTeas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Flavors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BaseTea_Id = c.Int(),
                        Flavor_Id = c.Int(),
                        Size_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseTeas", t => t.BaseTea_Id)
                .ForeignKey("dbo.Flavors", t => t.Flavor_Id)
                .ForeignKey("dbo.TeaSizes", t => t.Size_Id)
                .Index(t => t.BaseTea_Id)
                .Index(t => t.Flavor_Id)
                .Index(t => t.Size_Id);
            
            CreateTable(
                "dbo.TeaSizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Toppings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(nullable: false),
                        Name = c.String(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Toppings", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Size_Id", "dbo.TeaSizes");
            DropForeignKey("dbo.Orders", "Flavor_Id", "dbo.Flavors");
            DropForeignKey("dbo.Orders", "BaseTea_Id", "dbo.BaseTeas");
            DropIndex("dbo.Toppings", new[] { "Order_Id" });
            DropIndex("dbo.Orders", new[] { "Size_Id" });
            DropIndex("dbo.Orders", new[] { "Flavor_Id" });
            DropIndex("dbo.Orders", new[] { "BaseTea_Id" });
            DropTable("dbo.Toppings");
            DropTable("dbo.TeaSizes");
            DropTable("dbo.Orders");
            DropTable("dbo.Flavors");
            DropTable("dbo.Customers");
            DropTable("dbo.BaseTeas");
        }
    }
}
