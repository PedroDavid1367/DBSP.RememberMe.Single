using DBSP.RememberMe.API.DAL;
using DBSP.RememberMe.API.DAL.Managers;
using System;

namespace DBSP.RememberMe.API.DAL
{
  public class UnitOfWork : IDisposable
  {
    private RememberMeDbContext _db;
    public NotesManager NotesManager { get; private set; }
    public ContactsManager ContactsManager { get; private set; }
    private bool _disposed = false;

    public UnitOfWork()
    {
      _db = new RememberMeDbContext();
      NotesManager = new NotesManager(_db);
      ContactsManager = new ContactsManager(_db);
    }

    public void Complete()
    {
      _db.SaveChanges();
    }

    public void Dispose()
    {
      System.Diagnostics.Debug.WriteLine("Dispose() was called on UnitOfWork for API");
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      System.Diagnostics.Debug.WriteLine("Dispose(bool disposing) was called on UnitOfWork for API");
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
