namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeVotNameToStaffName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Voters", "StaffName", c => c.String());
            DropColumn("dbo.Voters", "VotName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Voters", "VotName", c => c.String());
            DropColumn("dbo.Voters", "StaffName");
        }
    }
}
