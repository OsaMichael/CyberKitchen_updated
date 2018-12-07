namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStaffNoToRatingRemoeve : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ratings", "StaffNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "StaffNo", c => c.String());
        }
    }
}
