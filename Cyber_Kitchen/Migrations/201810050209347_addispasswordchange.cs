namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addispasswordchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsPasswordChange", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsPasswordChange");
        }
    }
}
