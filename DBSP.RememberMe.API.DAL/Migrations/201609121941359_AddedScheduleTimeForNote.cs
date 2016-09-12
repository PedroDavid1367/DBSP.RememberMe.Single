namespace DBSP.RememberMe.API.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedScheduleTimeForNote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "ScheduleTime", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "ScheduleTime");
        }
    }
}
