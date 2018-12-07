namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLogin : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ratings", "RankId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "RankId", c => c.Int());
        }
    }
}
