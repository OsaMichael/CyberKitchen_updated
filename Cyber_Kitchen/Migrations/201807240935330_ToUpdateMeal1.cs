namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToUpdateMeal1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Meals", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meals", "UserId", c => c.String());
        }
    }
}
