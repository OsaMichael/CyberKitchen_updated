namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStaffIdNew : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Meals", "Users_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Meals", new[] { "Users_Id" });
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "StaffId", c => c.Int());
            DropColumn("dbo.Meals", "Users_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meals", "Users_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "StaffId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Discriminator");
            CreateIndex("dbo.Meals", "Users_Id");
            AddForeignKey("dbo.Meals", "Users_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
