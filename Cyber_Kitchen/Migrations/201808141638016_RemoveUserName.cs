namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserName : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ratings", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "UserName", c => c.String());
        }
    }
}
