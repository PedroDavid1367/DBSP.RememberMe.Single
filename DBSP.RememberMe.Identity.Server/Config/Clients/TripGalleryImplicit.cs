using IdentityServer3.Core.Models;

namespace DBSP.RememberMe.Identity.Server.Config.Clients
{
  public class TripGalleryImplicit
  {
    public const string ClientId = "tripgalleryimplicit";
    public const string ClientName = "Trip Gallery (Implicit - Angular1)";
    public const Flows Flow = Flows.Implicit;
    public const bool AllowAccessToAllScopes = true;
    public const bool RequireConsent = false;

    public const string RedirectUriCallback = "http://localhost:15745/callback.html";
    public const string RedirectUriRefresh = "http://localhost:15745/silentrefreshframe.html";

    public const string PostLogoutUri = "http://localhost:15745/";
  }
}