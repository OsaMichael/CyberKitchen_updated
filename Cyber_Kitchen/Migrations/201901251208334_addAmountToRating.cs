namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAmountToRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "IsChecked", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ratings", "AmountPay", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Ratings", "IsBackTo", c => c.Boolean(nullable: false));
            AddColumn("dbo.RatingModels", "AmountPay", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RatingModels", "IsBackTo", c => c.Boolean(nullable: false));
            AddColumn("dbo.RatingModels", "IsChecked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RatingModels", "IsChecked");
            DropColumn("dbo.RatingModels", "IsBackTo");
            DropColumn("dbo.RatingModels", "AmountPay");
            DropColumn("dbo.Ratings", "IsBackTo");
            DropColumn("dbo.Ratings", "AmountPay");
            DropColumn("dbo.Ratings", "IsChecked");
        }
    }
}
