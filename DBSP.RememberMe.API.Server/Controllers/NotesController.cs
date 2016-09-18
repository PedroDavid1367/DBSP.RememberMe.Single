using DBSP.RememberMe.API.DAL;
using DBSP.RememberMe.API.Model;
using DBSP.RememberMe.API.Server.Helpers;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData;
using System.Web.OData.Routing;

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

    [EnableQuery(MaxExpansionDepth = 3, MaxTop = 5, PageSize = 4)]
    public IHttpActionResult Get()
    {
      try
      {
        string ownerId = TokenIdentityHelper.GetOwnerIdFromToken();
        var notes = UnitOfWork.NotesManager.GetNotes(ownerId);
        return Ok(notes);
      }
      catch (Exception)
      {
        return InternalServerError();
      }
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
        //note.OwnerId = ownerId;
        var createdNote = UnitOfWork.NotesManager.AddNote(ownerId, note);
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

        string ownerId = TokenIdentityHelper.GetOwnerIdFromToken();

        var tuple = UnitOfWork.NotesManager.GetNoteById(ownerId, key);

        if (tuple.Item1 == null && tuple.Item2 == false)
        {
          return NotFound();
        }

        if (tuple.Item1 != null && tuple.Item2 == false)
        {
          return StatusCode(HttpStatusCode.Forbidden);
        }

        var note = tuple.Item1;

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
        string ownerId = TokenIdentityHelper.GetOwnerIdFromToken();

        var tuple = UnitOfWork.NotesManager.GetNoteById(ownerId, key);

        if (tuple.Item1 == null && tuple.Item2 == false)
        {
          return NotFound();
        }

        if (tuple.Item1 != null && tuple.Item2 == false)
        {
          return StatusCode(HttpStatusCode.Forbidden);
        }

        var note = tuple.Item1;

        UnitOfWork.NotesManager.RemoveNote(note);
        UnitOfWork.Complete();

        return StatusCode(HttpStatusCode.NoContent);
      }
      catch (Exception)
      {
        return InternalServerError();
      }
    }

    [HttpGet]
    [ODataRoute("Notes/RememberMe.Functions.GetNotesCount()")]
    public IHttpActionResult GetNotesCount()
    {
      // Gets the number of notes contained in the db.
      var count = 0;
      try
      {
        string ownerId = TokenIdentityHelper.GetOwnerIdFromToken();

        count = UnitOfWork.NotesManager.GetNotes(ownerId).ToList().Count;
      }
      catch (Exception)
      {
        return InternalServerError();
      }
      return Ok(count);
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