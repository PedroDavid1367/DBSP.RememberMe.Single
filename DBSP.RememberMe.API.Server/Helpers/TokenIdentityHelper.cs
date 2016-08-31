using System.Security.Claims;
using System.Web;

namespace DBSP.RememberMe.API.Server.Helpers
{
  public static class TokenIdentityHelper
  {
    public static string GetOwnerIdFromToken()
    {
      var identity = HttpContext.Current.User.Identity as ClaimsIdentity;

      if (identity == null)
        return null;

      var issuerFromIdentity = identity.FindFirst("iss");
      var subFromIdentity = identity.FindFirst("sub");

      if (issuerFromIdentity == null || subFromIdentity == null)
        return null;

      return issuerFromIdentity.Value + subFromIdentity.Value;
    }
  }
}
