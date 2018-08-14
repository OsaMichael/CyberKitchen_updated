namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ratings", "UserId");
            AddForeignKey("dbo.Ratings", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Ratings", "FirstName");
            DropColumn("dbo.Ratings", "LastName");
            DropColumn("dbo.Ratings", "UserPro_FirstName");
            DropColumn("dbo.Ratings", "UserPro_LastName");
            DropColumn("dbo.Ratings", "UserPro_Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "UserPro_Email", c => c.String());
            AddColumn("dbo.Ratings", "UserPro_LastName", c => c.String());
            AddColumn("dbo.Ratings", "UserPro_FirstName", c => c.String());
            AddColumn("dbo.Ratings", "LastName", c => c.String());
            AddColumn("dbo.Ratings", "FirstName", c => c.String());
            DropForeignKey("dbo.Ratings", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropColumn("dbo.Ratings", "UserId");
        }
    }
}
