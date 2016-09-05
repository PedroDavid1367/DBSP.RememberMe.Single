using IdentityServer3.Core.Models;

namespace DBSP.RememberMe.Identity.Server.Config.Clients
{
  public class RememberMeImplicit
  {
    public const string ClientId = "remembermeimplicit";
    public const string ClientName = "RememberMe (Implicit Flow)";
    public const Flows Flow = Flows.Implicit;
    public const bool AllowAccessToAllScopes = true;
    public const bool RequireConsent = false;

    public const string RedirectUriCallback = "http://localhost:8888/callback";
    public const string RedirectUriRefresh = "http://localhost:8888/silent-refresh";

    public const string PostLogoutUri = "http://localhost:8888/home";
  }
}