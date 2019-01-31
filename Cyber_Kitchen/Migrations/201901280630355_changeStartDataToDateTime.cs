namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeStartDataToDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Histories", "StartDate", c => c.String());
            AlterColumn("dbo.Histories", "EndDate", c => c.String());
            AlterColumn("dbo.PeriodModels", "StartDate", c => c.String());
            AlterColumn("dbo.Periods", "StartDate", c => c.String());
            AlterColumn("dbo.Periods", "EndDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Periods", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Periods", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PeriodModels", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Histories", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Histories", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
