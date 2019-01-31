namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddupdateHistorydatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Histories", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Histories", "EndDate", c => c.String());
        }
    }
}
