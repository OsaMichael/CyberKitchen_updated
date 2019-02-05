namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUPDATEAMOUNTTONOMAL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AmountPrices", "RestId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AmountPrices", "RestId");
        }
    }
}
