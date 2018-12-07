namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "Sid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ratings", "Sid");
        }
    }
}
