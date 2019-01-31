namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddupdateHistorydatetimemore : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Periods", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Periods", "EndDate", c => c.String());
        }
    }
}
