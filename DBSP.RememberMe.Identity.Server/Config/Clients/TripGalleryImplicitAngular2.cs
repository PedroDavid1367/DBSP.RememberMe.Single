using IdentityServer3.Core.Models;

namespace DBSP.RememberMe.Identity.Server.Config.Clients
{
  public class TripGalleryImplicitAngular2
  {
    public const string ClientId = "tripgalleryimplicitAngular2";
    public const string ClientName = "Trip Gallery (Implicit - Angular2)";
    public const Flows Flow = Flows.Implicit;
    public const bool AllowAccessToAllScopes = true;
    public const bool RequireConsent = false;

    public const string RedirectUriCallback = "http://localhost:8080/callback";
    public const string RedirectUriRefresh = "http://localhost:8080/silent-refresh";

    public const string PostLogoutUri = "http://localhost:15745/home";
  }
}