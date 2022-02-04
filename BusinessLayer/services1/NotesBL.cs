using BusinessLayer.InterfacesBL;
using CommanLayer.Models1;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entites;
using RepositoryLayer.InterfacesRL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services1
{
    public class NotesBL : INotesBL
    {
        INotesRL notesRL;
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }
        public bool CreateNotes(NotesModel notesModel, long userId)
        {
            try
            {
                return notesRL.CreateNotes(notesModel, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Notes> GetAllNotes()
        {
            try
            {
                return notesRL.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Notes> GetAllNotesUsingRedisCache()
        {
            try
            {
                return notesRL.GetAllNotesUsingRedisCache();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Notes> GetAllNotesByUserID(int id)
        {
            try
            {
                return notesRL.GetAllNotesByUserID(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteNote(int notesID)
        {
            try
            {
                if (notesRL.DeleteNote(notesID))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateNotes(int noteID, UpdateNotesModel notesModel)
        {
            try
            {
                if (notesRL.UpdateNotes(noteID, notesModel))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Colorchange(long userId, long noteID, string color)
        {
            try
            {
                return notesRL.Colorchange(userId, noteID, color);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ArchieveChange(long userId, long noteID)
        {
            try
            {
                return notesRL.ArchieveChange(userId, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool PinChange(long userId, long noteID)
        {
            try
            {
                return notesRL.PinChange(userId, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool TrashChange(long userId, long noteID)
        {
            try
            {
                return notesRL.TrashChange(userId, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UploadImage(long userId, long noteID, IFormFile file)
        {
            try
            {
                return notesRL.UploadImage(userId, noteID, file);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

