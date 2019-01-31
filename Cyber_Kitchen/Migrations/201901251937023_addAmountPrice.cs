namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAmountPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RestaurantModels", "Taste", c => c.Int(nullable: false));
            AddColumn("dbo.RestaurantModels", "Quality", c => c.Int(nullable: false));
            AddColumn("dbo.RestaurantModels", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.RestaurantModels", "TimeLiness", c => c.Int(nullable: false));
            AddColumn("dbo.RestaurantModels", "CustomerServices", c => c.Int(nullable: false));
            DropColumn("dbo.Restaurants", "Price");
            DropColumn("dbo.RestaurantModels", "Price");
            DropColumn("dbo.RestaurantModels", "IsCanceled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RestaurantModels", "IsCanceled", c => c.Boolean(nullable: false));
            AddColumn("dbo.RestaurantModels", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Restaurants", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.RestaurantModels", "CustomerServices");
            DropColumn("dbo.RestaurantModels", "TimeLiness");
            DropColumn("dbo.RestaurantModels", "Quantity");
            DropColumn("dbo.RestaurantModels", "Quality");
            DropColumn("dbo.RestaurantModels", "Taste");
        }
    }
}
