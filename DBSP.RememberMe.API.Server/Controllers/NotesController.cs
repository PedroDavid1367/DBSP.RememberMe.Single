using DBSP.RememberMe.API.DAL;
using DBSP.RememberMe.API.Model;
using DBSP.RememberMe.API.Server.Helpers;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData;

namespace DBSP.RememberMe.API.Server.Controllers
{
  [Authorize]
  public class NotesController : ODataController
  {
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
        var createdNote = UnitOfWork.NotesManager.AddNote(note);
        UnitOfWork.Complete();

        return Created(createdNote);
      }
      catch (Exception)
      {
        return InternalServerError();
      }      
    }

    public IHttpActionResult Patch([FromODataUri] int key, Delta<Note> patch)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return BadRequest(ModelState);
        }

        var note = UnitOfWork.NotesManager.GetNoteById(key);

        if (note == null)
        {
          return NotFound();
        }

        // So that no one can change Id.
        var id = note.Id;
        patch.Patch(note);
        note.Id = id;
        UnitOfWork.Complete();

        return StatusCode(HttpStatusCode.NoContent);
      }
      catch (Exception)
      {
        return InternalServerError();
      }
    }

    public IHttpActionResult Delete([FromODataUri] int key)
    {
      try
      {
        var note = UnitOfWork.NotesManager.GetNoteById(key);

        if (note == null)
        {
          return NotFound();
        }

        UnitOfWork.NotesManager.RemoveNote(note);
        UnitOfWork.Complete();

        return StatusCode(HttpStatusCode.NoContent);
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