using DBSP.RememberMe.API.DAL;
using DBSP.RememberMe.API.Model;
using DBSP.RememberMe.API.Server.Helpers;
using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData;

namespace DBSP.RememberMe.API.Server.Controllers
{
  [Authorize]
  [EnableCors("http://localhost:15745 , http://localhost:8080", "*", "GET, POST, PATCH")]
  public class NotesController : ODataController
  {
    //private RememberMeDbContext _ctx = new RememberMeDbContext();
    private UnitOfWork UnitOfWork { get; set; }
    private bool _disposed;

    public NotesController() : this(new UnitOfWork())
    {
    }

    public NotesController(UnitOfWork unitOfWork)
    {
      UnitOfWork = unitOfWork;
    }

    [EnableQuery(MaxExpansionDepth = 3, MaxSkip = 10, MaxTop = 5, PageSize = 4)]
    public IHttpActionResult Get()
    {
      //return Ok(_ctx.Notes);
      var notes = UnitOfWork.NotesManager.GetNotes();
      return Ok(notes);
    }

    public IHttpActionResult Post(Note note)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return BadRequest(ModelState);
        }

        string ownerId = TokenIdentityHelper.GetOwnerIdFromToken();
        note.OwnerId = ownerId;
        //var createdNote = _ctx.Notes.Add(note);
        //_ctx.SaveChanges();
        var createdNote = UnitOfWork.NotesManager.AddNote(note);
        UnitOfWork.Complete();

        return Created(createdNote);
      }
      catch (Exception)
      {
        return InternalServerError();
      }      
    }

    protected override void Dispose(bool disposing)
    {
      System.Diagnostics.Debug.WriteLine("Dispose(bool disposing) was called on NotesController");
      if (!_disposed)
      {
        if (disposing && UnitOfWork != null)
        {
          UnitOfWork.Dispose();
          UnitOfWork = null;
        }
      }

      _disposed = true;
      base.Dispose(disposing);
    }
  }
}