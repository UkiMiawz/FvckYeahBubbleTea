namespace FvckYeahBubbleTea.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrder : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Sizes", newName: "TeaSizes");
            DropForeignKey("dbo.Orders", "FinalOrder_Id", "dbo.FinalOrders");
            DropIndex("dbo.Orders", new[] { "FinalOrder_Id" });
            DropColumn("dbo.Orders", "FinalOrder_Id");
            DropTable("dbo.FinalOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FinalOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "FinalOrder_Id", c => c.Int());
            CreateIndex("dbo.Orders", "FinalOrder_Id");
            AddForeignKey("dbo.Orders", "FinalOrder_Id", "dbo.FinalOrders", "Id");
            RenameTable(name: "dbo.TeaSizes", newName: "Sizes");
        }
    }
}
