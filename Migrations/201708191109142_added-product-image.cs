namespace selfCoding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedproductimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageFile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageFile");
        }
    }
}
