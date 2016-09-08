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
  public class ContactsController : ODataController
  {
    private UnitOfWork UnitOfWork { get; set; }
    private bool _disposed;

    public ContactsController() : this(new UnitOfWork())
    {
    }

    public ContactsController(UnitOfWork unitOfWork)
    {
      UnitOfWork = unitOfWork;
    }

    [EnableQuery(MaxExpansionDepth = 3, MaxTop = 5, PageSize = 4)]
    public IHttpActionResult Get()
    {
      var contacts = UnitOfWork.ContactsManager.GetContacts();
      return Ok(contacts);
    }

    public IHttpActionResult Post(Contact contact)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return BadRequest(ModelState);
        }

        string ownerId = TokenIdentityHelper.GetOwnerIdFromToken();
        contact.OwnerId = ownerId;
        var createdContact = UnitOfWork.ContactsManager.AddContact(contact);
        UnitOfWork.Complete();

        return Created(createdContact);
      }
      catch (Exception)
      {
        return InternalServerError();
      }      
    }

    public IHttpActionResult Patch([FromODataUri] int key, Delta<Contact> patch)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return BadRequest(ModelState);
        }

        var contact = UnitOfWork.ContactsManager.GetContactById(key);

        if (contact == null)
        {
          return NotFound();
        }

        // So that no one can change Id.
        var id = contact.Id;
        patch.Patch(contact);
        contact.Id = id;
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
        var contact = UnitOfWork.ContactsManager.GetContactById(key);

        if (contact == null)
        {
          return NotFound();
        }

        UnitOfWork.ContactsManager.RemoveContact(contact);
        UnitOfWork.Complete();

        return StatusCode(HttpStatusCode.NoContent);
      }
      catch (Exception)
      {
        return InternalServerError();
      }
    }

    [HttpGet]
    [ODataRoute("Contacts/RememberMe.Functions.GetContactsCount()")]
    public IHttpActionResult GetContactsCount()
    {
      // Gets the number of contacts contained in the db.
      var count = 0;
      try
      {
        count = UnitOfWork.ContactsManager.GetContacts().ToList().Count;
      }
      catch (Exception)
      {
        return InternalServerError();
      }
      return Ok(count);
    }

    protected override void Dispose(bool disposing)
    {
      System.Diagnostics.Debug.WriteLine("Dispose(bool disposing) was called on ContactsController");
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