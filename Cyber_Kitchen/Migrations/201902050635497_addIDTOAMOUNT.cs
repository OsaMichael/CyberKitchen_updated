namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIDTOAMOUNT : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.AmountPrices");
            AddColumn("dbo.AmountPrices", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.AmountPrices", "AmountPriceId", c => c.String());
            AddPrimaryKey("dbo.AmountPrices", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.AmountPrices");
            AlterColumn("dbo.AmountPrices", "AmountPriceId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AmountPrices", "ID");
            AddPrimaryKey("dbo.AmountPrices", "AmountPriceId");
        }
    }
}
