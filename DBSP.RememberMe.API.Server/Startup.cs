using DBSP.RememberMe.API.Server.Configuration;
using IdentityServer3.AccessTokenValidation;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;

namespace DBSP.RememberMe.API.Server
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

      app.UseIdentityServerBearerTokenAuthentication(
       new IdentityServerBearerTokenAuthenticationOptions
       {
         Authority = DBSP.RememberMe.API.Constants.RememberMeSTS,
         // Re factorize this scope to notes, reminders and contacts.
         RequiredScopes = new[] { "notesmanagement" },
       });

      var config = WebApiConfig.Register();

      app.UseWebApi(config);
    }
  }
}