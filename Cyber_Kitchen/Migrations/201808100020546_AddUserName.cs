namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "UserName", c => c.String());
            AddColumn("dbo.Ratings", "FirstName", c => c.String());
            AddColumn("dbo.Ratings", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ratings", "LastName");
            DropColumn("dbo.Ratings", "FirstName");
            DropColumn("dbo.Ratings", "UserName");
        }
    }
}
