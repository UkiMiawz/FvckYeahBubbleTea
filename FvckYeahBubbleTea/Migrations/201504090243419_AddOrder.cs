namespace FvckYeahBubbleTea.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FinalOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                        FinalOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseTeas", t => t.BaseTea_Id)
                .ForeignKey("dbo.Flavors", t => t.Flavor_Id)
                .ForeignKey("dbo.Sizes", t => t.Size_Id)
                .ForeignKey("dbo.FinalOrders", t => t.FinalOrder_Id)
                .Index(t => t.BaseTea_Id)
                .Index(t => t.Flavor_Id)
                .Index(t => t.Size_Id)
                .Index(t => t.FinalOrder_Id);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Toppings", "Order_Id", c => c.Int());
            CreateIndex("dbo.Toppings", "Order_Id");
            AddForeignKey("dbo.Toppings", "Order_Id", "dbo.Orders", "Id");
            DropColumn("dbo.BaseTeas", "Price");
            DropColumn("dbo.Flavors", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Flavors", "Price", c => c.Single(nullable: false));
            AddColumn("dbo.BaseTeas", "Price", c => c.Single(nullable: false));
            DropForeignKey("dbo.Orders", "FinalOrder_Id", "dbo.FinalOrders");
            DropForeignKey("dbo.Toppings", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Size_Id", "dbo.Sizes");
            DropForeignKey("dbo.Orders", "Flavor_Id", "dbo.Flavors");
            DropForeignKey("dbo.Orders", "BaseTea_Id", "dbo.BaseTeas");
            DropIndex("dbo.Toppings", new[] { "Order_Id" });
            DropIndex("dbo.Orders", new[] { "FinalOrder_Id" });
            DropIndex("dbo.Orders", new[] { "Size_Id" });
            DropIndex("dbo.Orders", new[] { "Flavor_Id" });
            DropIndex("dbo.Orders", new[] { "BaseTea_Id" });
            DropColumn("dbo.Toppings", "Order_Id");
            DropTable("dbo.Sizes");
            DropTable("dbo.Orders");
            DropTable("dbo.FinalOrders");
        }
    }
}
