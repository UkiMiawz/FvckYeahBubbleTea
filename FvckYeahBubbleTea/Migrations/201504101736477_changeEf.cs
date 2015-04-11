namespace FvckYeahBubbleTea.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeEf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "BaseTea_Id", "dbo.BaseTeas");
            DropForeignKey("dbo.Orders", "Flavor_Id", "dbo.Flavors");
            DropForeignKey("dbo.Orders", "Size_Id", "dbo.TeaSizes");
            DropIndex("dbo.Orders", new[] { "BaseTea_Id" });
            DropIndex("dbo.Orders", new[] { "Flavor_Id" });
            DropIndex("dbo.Orders", new[] { "Size_Id" });
            RenameColumn(table: "dbo.Orders", name: "BaseTea_Id", newName: "BaseTeaId");
            RenameColumn(table: "dbo.Orders", name: "Flavor_Id", newName: "FlavorId");
            RenameColumn(table: "dbo.Orders", name: "Size_Id", newName: "SizeId");
            AlterColumn("dbo.Orders", "BaseTeaId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "FlavorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "SizeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "SizeId");
            CreateIndex("dbo.Orders", "BaseTeaId");
            CreateIndex("dbo.Orders", "FlavorId");
            AddForeignKey("dbo.Orders", "BaseTeaId", "dbo.BaseTeas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "FlavorId", "dbo.Flavors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "SizeId", "dbo.TeaSizes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "SizeId", "dbo.TeaSizes");
            DropForeignKey("dbo.Orders", "FlavorId", "dbo.Flavors");
            DropForeignKey("dbo.Orders", "BaseTeaId", "dbo.BaseTeas");
            DropIndex("dbo.Orders", new[] { "FlavorId" });
            DropIndex("dbo.Orders", new[] { "BaseTeaId" });
            DropIndex("dbo.Orders", new[] { "SizeId" });
            AlterColumn("dbo.Orders", "SizeId", c => c.Int());
            AlterColumn("dbo.Orders", "FlavorId", c => c.Int());
            AlterColumn("dbo.Orders", "BaseTeaId", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "SizeId", newName: "Size_Id");
            RenameColumn(table: "dbo.Orders", name: "FlavorId", newName: "Flavor_Id");
            RenameColumn(table: "dbo.Orders", name: "BaseTeaId", newName: "BaseTea_Id");
            CreateIndex("dbo.Orders", "Size_Id");
            CreateIndex("dbo.Orders", "Flavor_Id");
            CreateIndex("dbo.Orders", "BaseTea_Id");
            AddForeignKey("dbo.Orders", "Size_Id", "dbo.TeaSizes", "Id");
            AddForeignKey("dbo.Orders", "Flavor_Id", "dbo.Flavors", "Id");
            AddForeignKey("dbo.Orders", "BaseTea_Id", "dbo.BaseTeas", "Id");
        }
    }
}
