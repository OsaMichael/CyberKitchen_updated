namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailToVoter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Voters", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Voters", "Email");
        }
    }
}
