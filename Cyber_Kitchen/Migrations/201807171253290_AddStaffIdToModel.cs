namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStaffIdToModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meals", "StaffId", c => c.Int());
            AddColumn("dbo.Meals", "Users_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Meals", "Users_Id");
            AddForeignKey("dbo.Meals", "Users_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Meals", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meals", "UserId", c => c.String());
            DropForeignKey("dbo.Meals", "Users_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Meals", new[] { "Users_Id" });
            DropColumn("dbo.Meals", "Users_Id");
            DropColumn("dbo.Meals", "StaffId");
        }
    }
}
