namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtoremoveerror : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "PeriodId", "dbo.Periods");
            DropIndex("dbo.Ratings", new[] { "PeriodId" });
            DropColumn("dbo.AspNetUsers", "IsProfessional");
            DropColumn("dbo.AspNetUsers", "StaffNo");
            DropColumn("dbo.Ratings", "PeriodId");
            DropTable("dbo.Periods");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Periods",
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
            
            AddColumn("dbo.Ratings", "PeriodId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "StaffNo", c => c.String());
            AddColumn("dbo.AspNetUsers", "IsProfessional", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Ratings", "PeriodId");
            AddForeignKey("dbo.Ratings", "PeriodId", "dbo.Periods", "PeriodId");
        }
    }
}
