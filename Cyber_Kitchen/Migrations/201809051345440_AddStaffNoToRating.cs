namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStaffNoToRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "StaffNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ratings", "StaffNo");
        }
    }
}
