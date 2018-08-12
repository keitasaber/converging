namespace Converging.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Product_Table_2018_12_08 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Link", c => c.String());
            DropColumn("dbo.Products", "Price");
            DropColumn("dbo.Products", "PromotionPrice");
            DropColumn("dbo.Products", "Warranty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Warranty", c => c.Int());
            AddColumn("dbo.Products", "PromotionPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Products", "Link");
        }
    }
}
