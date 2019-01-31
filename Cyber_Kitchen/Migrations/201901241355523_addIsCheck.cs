namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsCheck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "IsChecked", c => c.Boolean(nullable: false));
            AddColumn("dbo.RestaurantModels", "IsChecked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RestaurantModels", "IsChecked");
            DropColumn("dbo.Restaurants", "IsChecked");
        }
    }
}
