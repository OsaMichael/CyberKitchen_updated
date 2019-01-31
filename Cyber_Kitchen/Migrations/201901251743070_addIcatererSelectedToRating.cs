namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIcatererSelectedToRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "IsCatererSelected", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ratings", "IsMfongCominBack", c => c.String());
            AddColumn("dbo.RatingModels", "IsMfongCominBack", c => c.String());
            AddColumn("dbo.RatingModels", "IsCatererSelected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RatingModels", "IsCatererSelected");
            DropColumn("dbo.RatingModels", "IsMfongCominBack");
            DropColumn("dbo.Ratings", "IsMfongCominBack");
            DropColumn("dbo.Ratings", "IsCatererSelected");
        }
    }
}
