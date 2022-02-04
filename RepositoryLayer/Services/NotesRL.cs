using CommanLayer.Models1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entites;
using RepositoryLayer.InterfacesRL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRL : INotesRL
    {
        Context context;

        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;
        /// <summary>
        /// defining the NotesRL constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="config"></param>
        /// <param name="hostingEnvironment"></param>
        public NotesRL(Context context, IConfiguration config, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;//appcontext to for api
            this.configuration = config;//for startup file instance
            this.hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// it defines the creation of notes method to create a notes
        /// </summary>
        /// <param name="notesModel"></param>
        /// <param name="userId"></param>
        /// <returns true></returns>
        /// <returns false></returns>
        public bool CreateNotes(NotesModel notesModel, long userId)
        {
            try
            {
                Notes notes = new Notes();
                notes.Id = userId;
                notes.Title = notesModel.Title;
                notes.Message = notesModel.Message;
                notes.Remainder = notesModel.Remainder;
                notes.Color = notesModel.Color;
                notes.Image = notesModel.Image;
                notes.IsArchive = notesModel.IsArchive;
                notes.IsPin = notesModel.IsPin;
                notes.IsTrash = notesModel.IsTrash;
                context.Notes.Add(notes);
                var result = context.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// it defines the method to get all the notes
        /// </summary>
        /// <returns context.Notes.ToList></returns>
        public IEnumerable<Notes> GetAllNotes()
        {
            return context.Notes.ToList();
        }
        /// <summary>
        /// it defines the rediscache method to get all notes in faster way
        /// </summary>
        /// <returns context.Notes.ToList></returns>
        public IEnumerable<Notes> GetAllNotesUsingRedisCache()
        {
            return context.Notes.ToList();
        }
        /// <summary>
        /// it definines the method to get note by using user id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns ToList></returns>
        public IEnumerable<Notes> GetAllNotesByUserID(int id)
        {
            return context.Notes.Where(e => e.Id == id).ToList();
        }
        /// <summary>
        /// it defines the method for deletion of notes
        /// </summary>
        /// <param name="notesID"></param>
        /// <returns true></returns>
        /// <returns false></returns>
        public bool DeleteNote(int notesID)
        {
            Notes notes = context.Notes.Where(e => e.NoteId == notesID).FirstOrDefault();
            if (notes != null)
            {
                context.Notes.Remove(notes);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }


        }
        /// <summary>
        /// it defines the method to updating something in that note
        /// </summary>
        /// <param name="noteID"></param>
        /// <param name="notesModel"></param>
        /// <returns true></returns>
        /// <returns false></returns>
        public bool UpdateNotes(int noteID, UpdateNotesModel notesModel)
        {
            Notes notes = context.Notes.Where(e => e.NoteId == noteID).FirstOrDefault();
            notes.Title = notesModel.Title;
            notes.Message = notesModel.Message;
            notes.Remainder = notesModel.Remainder;
            notes.Color = notesModel.Color;
            notes.Image = notesModel.Image;
            notes.IsArchive = notesModel.IsArchive;
            notes.IsPin = notesModel.IsPin;
            notes.IsTrash = notesModel.IsTrash;
            context.Notes.Update(notes);
            var result = context.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// it defines the method to change the background colour of the note
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteID"></param>
        /// <param name="color"></param>
        /// <returns true></returns>
        /// <returns false></returns>
        public bool Colorchange(long userId, long noteID, string color)
        {
            try
            {
                Notes note = context.Notes.FirstOrDefault(e => e.Id == userId && e.NoteId == noteID);
                if (note != null)
                {
                    note.Color = color;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// it defines the method to save the changes of archivenote
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteID"></param>
        /// <returns true></returns>
        /// <returns false></returns>
        public bool ArchieveChange(long userId, long noteID)
        {
            try
            {
                Notes note = context.Notes.FirstOrDefault(e => e.Id == userId && e.NoteId == noteID);
                if (note != null)
                {
                    bool checkarchieve = note.IsArchive;
                    if (checkarchieve == true)
                    {
                        note.IsArchive = false;
                    }
                    if (checkarchieve == false)
                    {
                        note.IsArchive = true;
                    }
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        ///  it defines the method to save the changes of pinnote
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteID"></param>
        /// <returns true></returns>
        /// <returns false></returns>
        public bool PinChange(long userId, long noteID)
        {
            try
            {
                Notes note = context.Notes.FirstOrDefault(e => e.Id == userId && e.NoteId == noteID);
                if (note != null)
                {
                    bool checkpin = note.IsPin;
                    if (checkpin == true)
                    {
                        note.IsPin = false;
                    }
                    if (checkpin == false)
                    {
                        note.IsPin = true;
                    }
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        ///  it defines the method to save the changes of Trashnote
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteID"></param>
        /// <returns true></returns>
        /// <returns false></returns>

        public bool TrashChange(long userId, long noteID)
        {
            try
            {
                Notes note = context.Notes.FirstOrDefault(e => e.Id == userId && e.NoteId == noteID);
                if (note != null)
                {
                    bool checktrash = note.IsTrash;
                    if (checktrash == true)
                    {
                        note.IsTrash = false;
                    }
                    if (checktrash == false)
                    {
                        note.IsTrash = true;
                    }
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// it definesthe method to uploading the image in note
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteID"></param>
        /// <param name="file"></param>
        /// <returns true></returns>
        /// <returns false></returns>
        public bool UploadImage(long userId, long noteID, IFormFile file)
        {
            try
            {
                var target = Path.Combine(hostingEnvironment.ContentRootPath, "Images");
                Directory.CreateDirectory(target);
                var filePath = Path.Combine(target, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                Notes note = context.Notes.FirstOrDefault(e => e.Id == userId && e.NoteId == noteID);
                if (note!=null)
                {
                    note.Image = file.FileName;
                    var result = context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
   
}