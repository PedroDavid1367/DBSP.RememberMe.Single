using DBSP.RememberMe.Identity.DAL.Repositories;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSP.RememberMe.Identity.DAL
{
  public class UnitOfWork : IDisposable
  {
    private ApplicationDbContext _db;
    public UsersRepository UsersRepository { get; private set; }
    private bool _disposed = false;

    public UnitOfWork(ApplicationDbContext db, UsersRepository usersRepository)
    {
      _db = db;
      UsersRepository = usersRepository;
    }

    public void Complete()
    {
      _db.SaveChanges();
    }

    public static UnitOfWork Create(IdentityFactoryOptions<UnitOfWork> options,
      IOwinContext context)
    {
      return new UnitOfWork(context.Get<ApplicationDbContext>(), context.Get<UsersRepository>());
    }

    public void Dispose()
    {
      System.Diagnostics.Debug.WriteLine("Dispose() was called on UnitOfWork");
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      System.Diagnostics.Debug.WriteLine("Dispose(bool disposing) was called on UnitOfWork");
      if (!_disposed)
      {
        if (disposing)
        {
          _db.Dispose();
          _db = null;
        }
      }
      _disposed = true;
    }
  }
}
