namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTOupdategoinghome : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Periods");
        }
    }
}
