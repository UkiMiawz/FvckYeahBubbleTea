namespace FvckYeahBubbleTea.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BubbleTeaAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaseTeas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Flavors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Toppings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Toppings");
            DropTable("dbo.Friends");
            DropTable("dbo.Flavors");
            DropTable("dbo.BaseTeas");
        }
    }
}
