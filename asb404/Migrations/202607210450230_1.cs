namespace Asb404.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(nullable: false, maxLength: 40),
                        Ptext = c.String(nullable: false, maxLength: 80),
                        Link = c.String(nullable: false, maxLength: 50),
                        Image = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Gallaries",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idx = c.Int(nullable: false),
                        image = c.String(),
                        Discription = c.String(nullable: false),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Customer = c.String(nullable: false, maxLength: 80),
                        GroupId = c.Int(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 100),
                        NoProject = c.String(nullable: false, maxLength: 30),
                        noprivate = c.String(nullable: false, maxLength: 30),
                        Website = c.String(maxLength: 40),
                        Address = c.String(nullable: false, maxLength: 80),
                        Discription = c.String(maxLength: 2000),
                        image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subscribes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameFamily = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Mobail = c.String(),
                        Role = c.String(),
                        isApproved = c.Boolean(nullable: false),
                        Ostan = c.Int(nullable: false),
                        Shahrestan = c.String(),
                        Address = c.String(),
                        PostCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Subscribes");
            DropTable("dbo.Projects");
            DropTable("dbo.Groups");
            DropTable("dbo.Gallaries");
            DropTable("dbo.Banners");
        }
    }
}
