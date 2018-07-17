namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class all : DbMigration  
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestId = c.Int(),
                        UserId = c.String(),
                        Day = c.DateTime(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestId)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id)
                .Index(t => t.RestId)
                .Index(t => t.Users_Id);
            
            
        }
        
        public override void Down()
        {           
            DropIndex("dbo.Meals", new[] { "Users_Id" });
            DropIndex("dbo.Meals", new[] { "RestId" });            
            DropTable("dbo.Meals");          
        }
    }
}
