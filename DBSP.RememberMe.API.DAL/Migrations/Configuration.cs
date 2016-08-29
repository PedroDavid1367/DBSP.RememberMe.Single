namespace DBSP.RememberMe.API.DAL.Migrations
{
  using Model;
  using System;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;

  internal sealed class Configuration : DbMigrationsConfiguration<DBSP.RememberMe.API.DAL.RememberMeDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(DBSP.RememberMe.API.DAL.RememberMeDbContext context)
    {
      context.Notes.AddOrUpdate(n => n.Title,
        new Note()
        {
          Title = "C#",
          Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam neque urna, cursus non nibh sed, euismod convallis orci. Maecenas finibus sagittis justo, eu vehicula neque tempor convallis. Integer at diam pellentesque, auctor ipsum quis, sagittis est. Aenean iaculis, risus id fermentum congue, felis lectus dictum diam, non malesuada lacus odio eget ipsum. Curabitur eleifend volutpat sapien ac rutrum. Morbi elit velit, interdum quis semper ac, finibus rhoncus mauris. Morbi venenatis, massa et consectetur egestas, eros ante finibus augue, eu imperdiet justo dolor facilisis lorem. Nam ut elit nulla.",
          Category = "Programming Languages",
          Priority = 1,
          OwnerId = "Microsoft"
        },

        new Note()
        {
          Title = "JavaScript",
          Content = "Donec sodales sem nec consequat efficitur. Fusce aliquet ut enim in semper. Nullam eu lacus finibus nisi pellentesque posuere ac et justo. Mauris eu dapibus dui. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vivamus metus dolor, viverra a ipsum mattis, dictum semper enim. Mauris mattis nisl elit, imperdiet auctor risus vestibulum eget. Phasellus eget sem bibendum tortor tristique mollis. Quisque tristique nibh ultricies congue auctor. Vestibulum malesuada odio vitae lorem dapibus fermentum.",
          Category = "Programming Languages",
          Priority = 1,
          OwnerId = "W3C"
        }
      );
    }
  }
}
