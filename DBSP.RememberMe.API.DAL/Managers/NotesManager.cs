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

    public IQueryable<Note> GetNotes(string ownerId)
    {
      return _db.Notes.Where(n => n.OwnerId == ownerId);
    }

    public Note AddNote(Note note)
    {
      return _db.Notes.Add(note);
    }

    public Note AddNote(string ownerId, Note note)
    {
      note.OwnerId = ownerId;
      return _db.Notes.Add(note);
    }

    public Note GetNoteById(int id)
    {
      return _db.Notes.FirstOrDefault(n => n.Id == id);
    }

    public Tuple<Note, bool> GetNoteById(string ownerId, int id)
    {
      var res = new Tuple<Note, bool>(null, false);

      // A note exist and the ownerId is valid.
      var validNote = _db.Notes.FirstOrDefault(n => n.Id == id && n.OwnerId == ownerId);
      if (validNote != null)
      {
        return new Tuple<Note, bool>(validNote, true);
      }

      // A note exist and the ownerId is invalid.
      var invalidNote = _db.Notes.FirstOrDefault(n => n.Id == id);
      if (invalidNote != null)
      {
        return new Tuple<Note, bool>(invalidNote, false);
      }

      return res;
    }

    public void RemoveNote(Note note)
    {
      _db.Notes.Remove(note);
    }
  }
}
