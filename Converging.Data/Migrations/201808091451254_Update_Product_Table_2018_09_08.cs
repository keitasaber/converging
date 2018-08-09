namespace Converging.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Product_Table_2018_09_08 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductTags", "PostID", "dbo.Posts");
            DropIndex("dbo.ProductTags", new[] { "PostID" });
            DropPrimaryKey("dbo.ProductTags");
            AddColumn("dbo.ProductTags", "ProductID", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Tags", c => c.String());
            AddPrimaryKey("dbo.ProductTags", new[] { "ProductID", "TagID" });
            CreateIndex("dbo.ProductTags", "ProductID");
            AddForeignKey("dbo.ProductTags", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
            DropColumn("dbo.ProductTags", "PostID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductTags", "PostID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ProductTags", "ProductID", "dbo.Products");
            DropIndex("dbo.ProductTags", new[] { "ProductID" });
            DropPrimaryKey("dbo.ProductTags");
            DropColumn("dbo.Products", "Tags");
            DropColumn("dbo.ProductTags", "ProductID");
            AddPrimaryKey("dbo.ProductTags", new[] { "PostID", "TagID" });
            CreateIndex("dbo.ProductTags", "PostID");
            AddForeignKey("dbo.ProductTags", "PostID", "dbo.Posts", "ID", cascadeDelete: true);
        }
    }
}
