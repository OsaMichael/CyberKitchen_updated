namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDepartmentToVoter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Voters", "Department", c => c.String());
            AddColumn("dbo.VoterModels", "Department", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VoterModels", "Department");
            DropColumn("dbo.Voters", "Department");
        }
    }
}
