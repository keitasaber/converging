namespace Converging.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Database_2018_18_08 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Footers", "CreatedDate");
            DropColumn("dbo.Footers", "CreatedBy");
            DropColumn("dbo.Footers", "UpdatedDate");
            DropColumn("dbo.Footers", "UpdatedBy");
            DropColumn("dbo.Footers", "MetaKeyword");
            DropColumn("dbo.Footers", "MetaDescription");
            DropColumn("dbo.Footers", "Status");
            DropColumn("dbo.MenuGroups", "CreatedDate");
            DropColumn("dbo.MenuGroups", "CreatedBy");
            DropColumn("dbo.MenuGroups", "UpdatedDate");
            DropColumn("dbo.MenuGroups", "UpdatedBy");
            DropColumn("dbo.MenuGroups", "MetaKeyword");
            DropColumn("dbo.MenuGroups", "MetaDescription");
            DropColumn("dbo.MenuGroups", "Status");
            DropColumn("dbo.Menus", "CreatedDate");
            DropColumn("dbo.Menus", "CreatedBy");
            DropColumn("dbo.Menus", "UpdatedDate");
            DropColumn("dbo.Menus", "UpdatedBy");
            DropColumn("dbo.Menus", "MetaKeyword");
            DropColumn("dbo.Menus", "MetaDescription");
            DropColumn("dbo.Menus", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menus", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Menus", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Menus", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.Menus", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Menus", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Menus", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Menus", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.MenuGroups", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.MenuGroups", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.MenuGroups", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.MenuGroups", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.MenuGroups", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.MenuGroups", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.MenuGroups", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.Footers", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Footers", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Footers", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.Footers", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Footers", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Footers", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Footers", "CreatedDate", c => c.DateTime());
        }
    }
}
