using DBSP.RememberMe.Identity.DAL;
using DBSP.RememberMe.Identity.DAL.Managers;
using DBSP.RememberMe.Identity.DAL.Repositories;
using DBSP.RememberMe.Identity.Model;
using DBSP.RememberMe.Identity.Server.Config;
using DBSP.RememberMe.Identity.Server.Services;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace DBSP.RememberMe.Identity.Server
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      // Configuring the db context, user manager, users repository and unit of work
      // to use a single instance per request.
      app.CreatePerOwinContext(ApplicationDbContext.Create);
      app.CreatePerOwinContext<UserManager>(UserManager.Create);
      app.CreatePerOwinContext<UsersRepository>(UsersRepository.Create);
      app.CreatePerOwinContext<UnitOfWork>(UnitOfWork.Create);

      app.Map("/identity", idsrvApp =>
      {
        var corsPolicyService = new DefaultCorsPolicyService()
        {
          // Anyone is allowed to make a request.
          AllowAll = true
        };

        var defaultViewServiceOptions = new DefaultViewServiceOptions();
        defaultViewServiceOptions.CacheViews = false;

        // Setting up Identity Server Service.
        var idServerServiceFactory = new IdentityServerServiceFactory()
                              .UseInMemoryClients(CustomClients.Get())
                              .UseInMemoryScopes(CustomScopes.Get());
                            //.UseInMemoryUsers(CustomUsers.Get());

        idServerServiceFactory.CorsPolicyService = new
                  Registration<IdentityServer3.Core.Services.ICorsPolicyService>(corsPolicyService);

        idServerServiceFactory.ConfigureDefaultViewService(defaultViewServiceOptions);

        // Registrations on Identity Server Service.
        idServerServiceFactory.Register(new Registration<ApplicationDbContext>());

        idServerServiceFactory.Register(new Registration<UserStore<ApplicationUser>>(resolver =>
        {
          return new UserStore<ApplicationUser>(resolver.Resolve<ApplicationDbContext>());
        }));

        idServerServiceFactory.Register(new Registration<UserManager<ApplicationUser>>(resolver =>
        {
          return new UserManager(resolver.Resolve<UserStore<ApplicationUser>>());
        }));

        idServerServiceFactory.Register(new Registration<UsersRepository>(resolver =>
        {
          return new UsersRepository(resolver.Resolve<UserManager<ApplicationUser>>());
        }));

        idServerServiceFactory.Register(new Registration<UnitOfWork>(resolver =>
        {
          return new UnitOfWork(resolver.Resolve<ApplicationDbContext>(),
            resolver.Resolve<UsersRepository>());
        }));

        idServerServiceFactory.UserService = new Registration<IUserService, CustomUserService>();

        // Setting up Identity Server options.
        var options = new IdentityServerOptions
        {
          Factory = idServerServiceFactory,

          // Just for Angular 2 App testing.
          // TODO: Get valid SSL certificates.
          RequireSsl = false,

          SiteName = "Security Service Token for RememberMe application.",
          SigningCertificate = LoadCertificate(),
          IssuerUri = DBSP.RememberMe.Identity.Constants.TripGalleryIssuerUri,
          PublicOrigin = DBSP.RememberMe.Identity.Constants.TripGallerySTSOrigin,
          AuthenticationOptions = new AuthenticationOptions()
          {
            EnablePostSignOutAutoRedirect = true,
            LoginPageLinks = new List<LoginPageLink>()
            {
              new LoginPageLink()
              {
                Type= "createaccount",
                Text = "Create a new account",
                Href = "~/createuseraccount"
              }
            }
          },
          CspOptions = new CspOptions()
          {
            Enabled = false
            // once available, leave Enabled at true and use:
            // FrameSrc = "https://localhost:44318 https://localhost:44316"
            // or
            // FrameSrc = "*" for all URI's.
          }
        };

        idsrvApp.UseIdentityServer(options);
      });
    }

    private X509Certificate2 LoadCertificate()
    {
      return new X509Certificate2(
          string.Format(@"{0}\certificates\idsrv3test.pfx",
          AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
    }
  }
}