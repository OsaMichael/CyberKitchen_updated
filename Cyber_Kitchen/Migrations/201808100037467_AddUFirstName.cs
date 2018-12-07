namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUFirstName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "UserPro_FirstName", c => c.String());
            AddColumn("dbo.Ratings", "UserPro_LastName", c => c.String());
            AddColumn("dbo.Ratings", "UserPro_Email", c => c.String());
            DropColumn("dbo.Ratings", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "UserName", c => c.String());
            DropColumn("dbo.Ratings", "UserPro_Email");
            DropColumn("dbo.Ratings", "UserPro_LastName");
            DropColumn("dbo.Ratings", "UserPro_FirstName");
        }
    }
}
