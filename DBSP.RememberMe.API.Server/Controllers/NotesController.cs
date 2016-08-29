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
    private RememberMeDbContext _ctx = new RememberMeDbContext();

    [EnableQuery(MaxExpansionDepth = 3, MaxSkip = 10, MaxTop = 5, PageSize = 4)]
    public IHttpActionResult Get()
    {
      return Ok(_ctx.Notes);
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
        var createdNote = _ctx.Notes.Add(note);
        _ctx.SaveChanges();

        return Created(createdNote);
      }
      catch (Exception)
      {
        return InternalServerError();
      }      
    }

    protected override void Dispose(bool disposing)
    {
      _ctx.Dispose();
      base.Dispose(disposing);
    }
  }
}