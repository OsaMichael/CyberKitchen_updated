namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToUpdateMeal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meals", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meals", "UserId");
        }
    }
}
