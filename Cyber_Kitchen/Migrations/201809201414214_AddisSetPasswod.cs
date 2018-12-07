namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddisSetPasswod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Voters", "isPasswordChanged", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Voters", "isPasswordChanged");
        }
    }
}
