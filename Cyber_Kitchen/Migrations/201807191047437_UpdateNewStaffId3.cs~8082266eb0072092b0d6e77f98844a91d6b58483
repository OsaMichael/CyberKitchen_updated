namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNewStaffId3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "StaffId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "StaffId", c => c.Int());
        }
    }
}
