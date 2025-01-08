namespace Product_34.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product34Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        CId = c.Int(nullable: false, identity: true),
                        CName = c.String(),
                    })
                .PrimaryKey(t => t.CId);
            
            CreateTable(
                "dbo.Details",
                c => new
                    {
                        DId = c.Int(nullable: false, identity: true),
                        PId = c.Int(nullable: false),
                        CId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DId)
                .ForeignKey("dbo.Colors", t => t.CId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.PId, cascadeDelete: true)
                .Index(t => t.PId)
                .Index(t => t.CId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        PId = c.Int(nullable: false, identity: true),
                        PName = c.String(),
                        Price = c.Int(nullable: false),
                        IsAviable = c.Boolean(nullable: false),
                        Pdate = c.DateTime(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.PId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Details", "PId", "dbo.Products");
            DropForeignKey("dbo.Details", "CId", "dbo.Colors");
            DropIndex("dbo.Details", new[] { "CId" });
            DropIndex("dbo.Details", new[] { "PId" });
            DropTable("dbo.Products");
            DropTable("dbo.Details");
            DropTable("dbo.Colors");
        }
    }
}
