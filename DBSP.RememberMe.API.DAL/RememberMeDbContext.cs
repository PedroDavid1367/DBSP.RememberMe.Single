using DBSP.RememberMe.API.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSP.RememberMe.API.DAL
{
  public class RememberMeDbContext : DbContext
  {
    public DbSet<Note> Notes { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    public RememberMeDbContext() : base("RememberMeAPI")
    {
      // disable lazy loading
      Configuration.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Note>();
      modelBuilder.Entity<Contact>();
    }
  }
}
