namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeDepartmtAD : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RatingModels", "PeriodId", "dbo.PeriodModels");
            DropIndex("dbo.RatingModels", new[] { "PeriodId" });
            AddColumn("dbo.PeriodModels", "Rating_RatId", c => c.Int());
            AddColumn("dbo.RatingModels", "Periods_PeriodId", c => c.Int());
            AddColumn("dbo.RatingModels", "PeriodModel_PeriodId", c => c.Int());
            CreateIndex("dbo.PeriodModels", "Rating_RatId");
            CreateIndex("dbo.RatingModels", "Periods_PeriodId");
            CreateIndex("dbo.RatingModels", "PeriodModel_PeriodId");
            AddForeignKey("dbo.PeriodModels", "Rating_RatId", "dbo.RatingModels", "RatId");
            AddForeignKey("dbo.RatingModels", "PeriodModel_PeriodId", "dbo.PeriodModels", "PeriodId");
            AddForeignKey("dbo.RatingModels", "Periods_PeriodId", "dbo.PeriodModels", "PeriodId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RatingModels", "Periods_PeriodId", "dbo.PeriodModels");
            DropForeignKey("dbo.RatingModels", "PeriodModel_PeriodId", "dbo.PeriodModels");
            DropForeignKey("dbo.PeriodModels", "Rating_RatId", "dbo.RatingModels");
            DropIndex("dbo.RatingModels", new[] { "PeriodModel_PeriodId" });
            DropIndex("dbo.RatingModels", new[] { "Periods_PeriodId" });
            DropIndex("dbo.PeriodModels", new[] { "Rating_RatId" });
            DropColumn("dbo.RatingModels", "PeriodModel_PeriodId");
            DropColumn("dbo.RatingModels", "Periods_PeriodId");
            DropColumn("dbo.PeriodModels", "Rating_RatId");
            CreateIndex("dbo.RatingModels", "PeriodId");
            AddForeignKey("dbo.RatingModels", "PeriodId", "dbo.PeriodModels", "PeriodId");
        }
    }
}
