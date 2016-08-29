using Microsoft.AspNet.Identity.EntityFramework;

namespace DBSP.RememberMe.Identity.Model
{
  public class ApplicationUser : IdentityUser
  {
    public bool IsActive { get; set; }
    public string Password { get; set; }
  }
}

