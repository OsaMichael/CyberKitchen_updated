namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdtoVoter : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Voters", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Voters", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Voters", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Voters", name: "UserId", newName: "User_Id");
        }
    }
}
