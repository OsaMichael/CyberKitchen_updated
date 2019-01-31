namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPeriod : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PeriodModels",
                c => new
                    {
                        PeriodId = c.Int(nullable: false, identity: true),
                        PeriodName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Discription = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PeriodId);
            
            CreateTable(
                "dbo.RatingModels",
                c => new
                    {
                        RatId = c.Int(nullable: false, identity: true),
                        VoterId = c.Int(),
                        RestId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        PeriodId = c.Int(),
                        Sid = c.String(),
                        Taste = c.Int(nullable: false),
                        Quality = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TimeLiness = c.Int(nullable: false),
                        CustomerServices = c.Int(nullable: false),
                        TotalScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Message = c.String(),
                        ImageUrl = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.RatId)
                .ForeignKey("dbo.PeriodModels", t => t.PeriodId)
                .ForeignKey("dbo.RestaurantModels", t => t.RestId)
                .ForeignKey("dbo.VoterModels", t => t.VoterId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.VoterId)
                .Index(t => t.RestId)
                .Index(t => t.UserId)
                .Index(t => t.PeriodId);
            
            CreateTable(
                "dbo.RestaurantModels",
                c => new
                    {
                        RestId = c.Int(nullable: false, identity: true),
                        RestName = c.String(),
                        TotalScore = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RestId);
            
            CreateTable(
                "dbo.ScoreModels",
                c => new
                    {
                        ScoreId = c.Int(nullable: false, identity: true),
                        VoterId = c.Int(),
                        RestId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        Taste = c.Int(nullable: false),
                        Quality = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TimeLiness = c.Int(nullable: false),
                        CustomerServices = c.Int(nullable: false),
                        TotalScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ScoreId)
                .ForeignKey("dbo.RestaurantModels", t => t.RestId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.VoterModels", t => t.VoterId)
                .Index(t => t.VoterId)
                .Index(t => t.RestId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.VoterModels",
                c => new
                    {
                        VoterId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        StaffName = c.String(nullable: false),
                        StaffNo = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Message = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        RestaurantModel_RestId = c.Int(),
                    })
                .PrimaryKey(t => t.VoterId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.RestaurantModels", t => t.RestaurantModel_RestId)
                .Index(t => t.UserId)
                .Index(t => t.RestaurantModel_RestId);
            
            AddColumn("dbo.Ratings", "PeriodId", c => c.Int());
            CreateIndex("dbo.Ratings", "PeriodId");
            AddForeignKey("dbo.Ratings", "PeriodId", "dbo.PeriodModels", "PeriodId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "PeriodId", "dbo.PeriodModels");
            DropForeignKey("dbo.RatingModels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.VoterModels", "RestaurantModel_RestId", "dbo.RestaurantModels");
            DropForeignKey("dbo.ScoreModels", "VoterId", "dbo.VoterModels");
            DropForeignKey("dbo.VoterModels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RatingModels", "VoterId", "dbo.VoterModels");
            DropForeignKey("dbo.ScoreModels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ScoreModels", "RestId", "dbo.RestaurantModels");
            DropForeignKey("dbo.RatingModels", "RestId", "dbo.RestaurantModels");
            DropForeignKey("dbo.RatingModels", "PeriodId", "dbo.PeriodModels");
            DropIndex("dbo.VoterModels", new[] { "RestaurantModel_RestId" });
            DropIndex("dbo.VoterModels", new[] { "UserId" });
            DropIndex("dbo.ScoreModels", new[] { "UserId" });
            DropIndex("dbo.ScoreModels", new[] { "RestId" });
            DropIndex("dbo.ScoreModels", new[] { "VoterId" });
            DropIndex("dbo.RatingModels", new[] { "PeriodId" });
            DropIndex("dbo.RatingModels", new[] { "UserId" });
            DropIndex("dbo.RatingModels", new[] { "RestId" });
            DropIndex("dbo.RatingModels", new[] { "VoterId" });
            DropIndex("dbo.Ratings", new[] { "PeriodId" });
            DropColumn("dbo.Ratings", "PeriodId");
            DropTable("dbo.VoterModels");
            DropTable("dbo.ScoreModels");
            DropTable("dbo.RestaurantModels");
            DropTable("dbo.RatingModels");
            DropTable("dbo.PeriodModels");
        }
    }
}
