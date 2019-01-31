namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addupdate26 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AmountPrices", "RestId", c => c.Int());
            AddColumn("dbo.Ratings", "AmountPriceId", c => c.String());
            AddColumn("dbo.RatingModels", "AmountPriceId", c => c.String());
            AddColumn("dbo.SummaryReports", "AmountPriceId", c => c.String());
            AddColumn("dbo.SummaryReports", "IsMfongComingBack", c => c.String());
            AddColumn("dbo.SummaryReports", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SummaryReports", "CreatedBy");
            DropColumn("dbo.SummaryReports", "IsMfongComingBack");
            DropColumn("dbo.SummaryReports", "AmountPriceId");
            DropColumn("dbo.RatingModels", "AmountPriceId");
            DropColumn("dbo.Ratings", "AmountPriceId");
            DropColumn("dbo.AmountPrices", "RestId");
        }
    }
}
