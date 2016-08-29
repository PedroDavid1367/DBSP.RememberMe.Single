namespace DBSP.RememberMe.API.DAL.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class InitialSetup : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.Notes",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            OwnerId = c.String(nullable: false, maxLength: 100),
            Title = c.String(nullable: false, maxLength: 100),
            Content = c.String(maxLength: 3000),
            Category = c.String(nullable: false, maxLength: 50),
            Priority = c.Int(nullable: false),
          })
          .PrimaryKey(t => t.Id);

    }

    public override void Down()
    {
      DropTable("dbo.Notes");
    }
  }
}
