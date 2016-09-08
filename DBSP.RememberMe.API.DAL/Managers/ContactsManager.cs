using DBSP.RememberMe.API.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSP.RememberMe.API.DAL.Managers
{
  public class ContactsManager
  {
    private readonly RememberMeDbContext _db;

    public ContactsManager(RememberMeDbContext db)
    {
      _db = db;
    }

    public DbSet<Contact> GetContacts()
    {
      return _db.Contacts;
    }

    public Contact AddContact(Contact contact)
    {
      return _db.Contacts.Add(contact);
    }

    public Contact GetContactById(int id)
    {
      return _db.Contacts.FirstOrDefault(c => c.Id == id);
    }

    public void RemoveContact(Contact contact)
    {
      _db.Contacts.Remove(contact);
    }
  }
}
