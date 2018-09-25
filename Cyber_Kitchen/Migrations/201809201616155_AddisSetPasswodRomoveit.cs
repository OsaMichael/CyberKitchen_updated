namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddisSetPasswodRomoveit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Voters", "isPasswordChanged");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Voters", "isPasswordChanged", c => c.Boolean(nullable: false));
        }
    }
}
