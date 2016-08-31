using DBSP.RememberMe.API.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSP.RememberMe.API.DAL.Managers
{
  public class NotesManager
  {
    private readonly RememberMeDbContext _db;

    public NotesManager(RememberMeDbContext db)
    {
      _db = db;
    }

    public DbSet<Note> GetNotes()
    {
      return _db.Notes;
    }

    public Note AddNote(Note note)
    {
      return _db.Notes.Add(note);
    }

    //public IQueryable QueryPersonById(int id)
    //{
    //  var query = _db.Persons.Where(p => p.Id == id);
    //  return query.Any() ? query : null;
    //}

    //public IQueryable QueryPersonByName(string name)
    //{
    //  var query = _db.Persons.Where(p => p.Name == name);
    //  return query.Any() ? query : null;
    //}

    //public Person AddPerson(Person person)
    //{
    //  return _db.Persons.Add(person);
    //}
  }
}
