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

    public Note GetNoteById(int id)
    {
      return _db.Notes.FirstOrDefault(n => n.Id == id);
    }

    public void RemoveNote(Note note)
    {
      _db.Notes.Remove(note);
    }
  }
}
