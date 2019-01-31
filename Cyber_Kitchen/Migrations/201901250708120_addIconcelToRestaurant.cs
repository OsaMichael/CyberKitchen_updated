namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIconcelToRestaurant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RestaurantModels", "IsCanceled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RestaurantModels", "IsCanceled");
        }
    }
}
