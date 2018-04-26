namespace selfCoding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Registers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UpdateYourProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UpdateUsername = c.String(),
                        UpdatePassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UpdateYourProfiles");
            DropTable("dbo.Registers");
            DropTable("dbo.Logins");
            DropTable("dbo.AddDetails");
        }
    }
}
