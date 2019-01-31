namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddupdateHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Histories", "EndDate", c => c.String());
            AlterColumn("dbo.PeriodModels", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Periods", "StartDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Periods", "StartDate", c => c.String());
            AlterColumn("dbo.PeriodModels", "StartDate", c => c.String());
            DropColumn("dbo.Histories", "EndDate");
        }
    }
}
