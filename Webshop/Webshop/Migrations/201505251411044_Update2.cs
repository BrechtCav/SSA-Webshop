namespace Webshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AvailableCultures",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Code = c.String(),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.ID);
            CreateTable(
                "dbo.Forms",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    Email = c.String(nullable: false),
                    Description = c.String(nullable: false),
                    NewFormTopic_ID = c.Int(),
                    NewOrder_ID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FormTopics", t => t.NewFormTopic_ID)
                .ForeignKey("dbo.Orders", t => t.NewOrder_ID)
                .Index(t => t.NewFormTopic_ID)
                .Index(t => t.NewOrder_ID);

            CreateTable(
                "dbo.FormTopics",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                })
                .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
        }
    }
}
