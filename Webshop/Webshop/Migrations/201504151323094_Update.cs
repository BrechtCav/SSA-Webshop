namespace Webshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        Username = c.String(),
                        BasketItem_ID = c.Int(),
                        Order_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BasketItems", t => t.BasketItem_ID)
                .ForeignKey("dbo.Orders", t => t.Order_ID)
                .Index(t => t.BasketItem_ID)
                .Index(t => t.Order_ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Total = c.Double(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.OrderUsers", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.OrderUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Name = c.String(),
                        FirstName = c.String(),
                        Street = c.String(),
                        Number = c.Int(nullable: false),
                        NumberAddition = c.String(),
                        ZipCode = c.Int(nullable: false),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_ID", "dbo.OrderUsers");
            DropForeignKey("dbo.OrderLines", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.OrderLines", "BasketItem_ID", "dbo.BasketItems");
            DropIndex("dbo.Orders", new[] { "User_ID" });
            DropIndex("dbo.OrderLines", new[] { "Order_ID" });
            DropIndex("dbo.OrderLines", new[] { "BasketItem_ID" });
            DropTable("dbo.OrderUsers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderLines");
        }
    }
}
