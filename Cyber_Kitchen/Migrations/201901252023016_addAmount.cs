namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAmount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AmountPrices",
                c => new
                    {
                        AmountPriceId = c.String(nullable: false, maxLength: 128),
                        IsMfongComingBack = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AmountPriceId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AmountPrices");
        }
    }
}
