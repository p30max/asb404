namespace Asb404.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactMessages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 200),
                        Subject = c.String(nullable: false, maxLength: 200),
                        Memo = c.String(nullable: false, maxLength: 4000),
                        Status = c.String(maxLength: 50),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactMessages");
        }
    }
}
