using DBSP.RememberMe.Identity.DAL.Managers;
using DBSP.RememberMe.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DBSP.RememberMe.Identity.DAL.Repositories
{
  public class UsersRepository : IDisposable
  {
    // TODO: Check if makes sense to use readonly.
    private UserManager<ApplicationUser> _userManager;
    private bool _disposed = false;

    public UsersRepository(UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
    }

    public ApplicationUser GetUser(string userName, string password)
    {
      return _userManager.Users
        .FirstOrDefault(u => u.UserName == userName && u.Password == password);
    }

    public ApplicationUser GetUserById(string subjectId)
    {
      return _userManager.Users.FirstOrDefault(u => u.Id == subjectId);
    }

    public IdentityResult CreateUser(ApplicationUser user, string password)
    {
      return _userManager.Create(user, password);
    }

    public void AddClaim(string userId, string claimType, string claimValue)
    {
      _userManager.AddClaim(userId, new Claim(claimType, claimValue));
    }

    public static UsersRepository Create(IdentityFactoryOptions<UsersRepository> options, 
      IOwinContext context)
    {
      return new UsersRepository(context.Get<UserManager>());
    }

    public void Dispose()
    {
      System.Diagnostics.Debug.WriteLine("Dispose() was called on UsersRepository");
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      System.Diagnostics.Debug.WriteLine("Dispose(bool disposing) was called on UsersRepository");
      if (!_disposed)
      {
        if (disposing)
        {
          _userManager.Dispose();
          _userManager = null;
        }
      }
      _disposed = true;
    }
  }
}
