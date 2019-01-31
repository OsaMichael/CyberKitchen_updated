namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeDepartmtRequire : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VoterModels", "Department", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VoterModels", "Department", c => c.String(nullable: false));
        }
    }
}
