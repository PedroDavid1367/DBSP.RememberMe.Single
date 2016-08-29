using DBSP.RememberMe.Identity.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DBSP.RememberMe.Identity.DAL
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext()
        : base("RememberMeIdentity", throwIfV1Schema: false)
    {
    }

    public static ApplicationDbContext Create()
    {
      return new ApplicationDbContext();
    }
  }
}

