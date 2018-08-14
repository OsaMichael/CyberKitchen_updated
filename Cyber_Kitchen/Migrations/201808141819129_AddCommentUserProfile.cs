namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentUserProfile : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserProfiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        FirstName = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FirstName);
            
        }
    }
}
