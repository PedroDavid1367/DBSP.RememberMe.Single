using DBSP.RememberMe.Identity.DAL;
using DBSP.RememberMe.Identity.Helpers;
using DBSP.RememberMe.Identity.Model;
using DBSP.RememberMe.Identity.Server.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace DBSP.RememberMe.Identity.Server.Controllers
{
  public class CreateUserAccountController : Controller
  {
    //private ApplicationUserManager _userManager;
    private UnitOfWork _uow;
    private bool _disposed = false;

    public CreateUserAccountController() { }

    //public CreateUserAccountController(ApplicationUserManager userManager)
    //{
    //  UserManager = userManager;
    //}

    public CreateUserAccountController(UnitOfWork unitOfWork)
    {
      UnitOfWork = unitOfWork;
    }

    public UnitOfWork UnitOfWork
    {
      get
      {
        return _uow ?? Request.GetOwinContext().Get<UnitOfWork>();
      }
      private set
      {
        _uow = value;
      }
    }

    //public ApplicationUserManager UserManager
    //{
    //  get
    //  {
    //    return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
    //  }
    //  private set
    //  {
    //    _userManager = value;
    //  }
    //}

    // GET: CreateUserAccount
    [HttpGet]
    public ActionResult Index(string signin)
    {
      return View(new CreateUserAccountModel());
    }

    [HttpPost]
    public ActionResult Index(string signin, CreateUserAccountModel model)
    {
      if (ModelState.IsValid)
      {
        // Create a user.
        var password = HashHelper.Sha512(model.UserName + model.Password);

        var user = new ApplicationUser
        {
          UserName = model.UserName,
          IsActive = true,
          Password = password,
          Email = model.Email
        };
        //var createdUser = UserManager.Create(user, model.Password);
        var createdUser = UnitOfWork.UsersRepository.CreateUser(user, model.Password);

        if (createdUser.Succeeded)
        {
          //var userRetrieved = UserManager.Users
          //  .FirstOrDefault(u => u.UserName == model.UserName && u.Password == password);
          var userRetrieved = UnitOfWork.UsersRepository.GetUser(model.UserName, password);

          if (userRetrieved != null)
          {
            //UserManager.AddClaim(userRetrieved.Id,
            //  new Claim(IdentityServer3.Core.Constants.ClaimTypes.GivenName, model.FirstName));
            //UserManager.AddClaim(userRetrieved.Id,
            //  new Claim(IdentityServer3.Core.Constants.ClaimTypes.FamilyName, model.LastName));
            //UserManager.AddClaim(userRetrieved.Id,
            //  new Claim(IdentityServer3.Core.Constants.ClaimTypes.Role, model.Role));
            //UserManager.AddClaim(userRetrieved.Id,
            //  new Claim(IdentityServer3.Core.Constants.ClaimTypes.Address, model.Address));
            //UserManager.AddClaim(userRetrieved.Id,
            //  new Claim(IdentityServer3.Core.Constants.ClaimTypes.Email, model.Email));
            UnitOfWork.UsersRepository.AddClaim(userRetrieved.Id,
              IdentityServer3.Core.Constants.ClaimTypes.GivenName, model.FirstName);
            UnitOfWork.UsersRepository.AddClaim(userRetrieved.Id,
              IdentityServer3.Core.Constants.ClaimTypes.FamilyName, model.LastName);
            UnitOfWork.UsersRepository.AddClaim(userRetrieved.Id,
              IdentityServer3.Core.Constants.ClaimTypes.Role, model.Role);
            UnitOfWork.UsersRepository.AddClaim(userRetrieved.Id,
              IdentityServer3.Core.Constants.ClaimTypes.Address, model.Address);
            UnitOfWork.UsersRepository.AddClaim(userRetrieved.Id,
              IdentityServer3.Core.Constants.ClaimTypes.Email, model.Email);
          }
        }
        // Redirect to the login page, passing in 
        // the signin parameter
        return Redirect("~/identity/" + IdentityServer3.Core.Constants.RoutePaths.Login + "?signin=" + signin);
      }
      return View();
    }

    protected override void Dispose(bool disposing)
    {
      System.Diagnostics.Debug.WriteLine("Dispose(bool disposing) was called on CreateUserAccountController");
      if (!_disposed)
      {
        if (disposing && _uow != null)
        {
          _uow.Dispose();
          _uow = null;
        }
      }

      _disposed = true;
      base.Dispose(disposing);
    }
  }
}