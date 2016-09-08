namespace DBSP.RememberMe.API.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactsInitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 100),
                        Organization = c.String(maxLength: 100),
                        Misc = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contacts");
        }
    }
}
