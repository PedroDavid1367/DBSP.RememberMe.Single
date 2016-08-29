namespace DBSP.RememberMe.API.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetupOwnerIdNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notes", "OwnerId", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "OwnerId", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
