namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcreatedbytohistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Histories", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Histories", "CreatedBy");
        }
    }
}
