using DBSP.RememberMe.Identity.Server.Config.Clients;
using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace DBSP.RememberMe.Identity.Server.Config
{
  public static class CustomClients
  {
    public static IEnumerable<Client> Get()
    {
      return new[]
      {
        new Client
        {
          ClientId = TripGalleryImplicit.ClientId,
          ClientName = TripGalleryImplicit.ClientName,
          Flow = TripGalleryImplicit.Flow,
          AllowAccessToAllScopes = TripGalleryImplicit.AllowAccessToAllScopes,
          //IdentityTokenLifetime = 10,
          //AccessTokenLifetime = 120,
          RequireConsent = TripGalleryImplicit.RequireConsent,

          RedirectUris = new List<string>
          {
            TripGalleryImplicit.RedirectUriCallback,
            TripGalleryImplicit.RedirectUriRefresh
          },
          PostLogoutRedirectUris = new List<string>()
          {
            TripGalleryImplicit.PostLogoutUri,
          }
        },
        new Client
        {
          ClientId = TripGalleryImplicitAngular2.ClientId,
          ClientName = TripGalleryImplicitAngular2.ClientName,
          Flow = TripGalleryImplicitAngular2.Flow,
          AllowAccessToAllScopes = TripGalleryImplicitAngular2.AllowAccessToAllScopes,
          //IdentityTokenLifetime = 10,
          //AccessTokenLifetime = 120,
          RequireConsent = TripGalleryImplicitAngular2.RequireConsent,

          RedirectUris = new List<string>
          {
            TripGalleryImplicitAngular2.RedirectUriCallback,
            TripGalleryImplicitAngular2.RedirectUriRefresh,
          },

          PostLogoutRedirectUris = new List<string>()
          {
            TripGalleryImplicitAngular2.PostLogoutUri,
          }
        }
      };
    }
  }
}
