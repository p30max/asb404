namespace Asb404.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sfkdf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "GroupId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "GroupId");
        }
    }
}
