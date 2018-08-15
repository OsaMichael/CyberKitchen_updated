namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTotalSToRest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "TotalScore", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "TotalScore");
        }
    }
}
