namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSIdToR : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropColumn("dbo.Ratings", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ratings", "UserId");
            AddForeignKey("dbo.Ratings", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
