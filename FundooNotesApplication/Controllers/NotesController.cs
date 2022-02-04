using BusinessLayer.InterfacesBL;
using CommanLayer.Models1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Http;

namespace FundooNotesDuplicate.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INotesBL notesBL;
        private readonly IMemoryCache memoryCache;
        private readonly Context context;
        private readonly IDistributedCache distributedCache;
        public NotesController(INotesBL notesBL, IMemoryCache memoryCache, Context context, IDistributedCache distributedCache)
        {
            this.notesBL = notesBL;
            this.memoryCache = memoryCache;
            this.context = context;
            this.distributedCache = distributedCache;

        }
        [Authorize]
        [HttpPost]
        public IActionResult CreateNotes(NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (notesBL.CreateNotes(notesModel, userId))
                {
                    return this.Ok(new { Success = true, message = "Notes created successfully", response = notesModel });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes creation unsuccessfull" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet]
        public IEnumerable<Notes> GetAllNotes()
        {
            try
            {
                return notesBL.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<Notes>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<Notes>>(serializedNotesList);
            }
            else
            {
                NotesList =  (List<Notes>)notesBL.GetAllNotes();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
            }
            return Ok(NotesList);
        }

    
        [Authorize]
        [HttpGet]
        public IEnumerable<Notes> GetNotesByUserID(int id)
        {
            try
            {
                return notesBL.GetAllNotesByUserID(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        public IActionResult UpdateNotes(int noteID, UpdateNotesModel notesModel)
        {
            try
            {
                if (notesBL.UpdateNotes(noteID, notesModel))
                {
                    return this.Ok(new { Success = true, message = "Notes updated successfully",response = notesModel,noteID});
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note with given ID not found" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteNote(int noteID)
        {
            try
            {
                if (notesBL.DeleteNote(noteID))
                {
                    return this.Ok(new { Success = true, message = "Notes deleted successfully" });

                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes with given ID not found" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        public IActionResult ColorNotes(long noteID, string color)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (notesBL.Colorchange(userId, noteID, color))
                {
                    return this.Ok(new { Success = true, message = "Color changed successfully",response = color,noteID});
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        public IActionResult ArchieveNotes(long noteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (notesBL.ArchieveChange(userId, noteID))
                {
                    return this.Ok(new { Success = true, message = "Archieve changed successfully",response = noteID });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        public IActionResult PinNotes(long noteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (notesBL.PinChange(userId, noteID))
                {
                    return this.Ok(new { Success = true, message = "Pin changed successfully",response = noteID });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        public IActionResult TrashNotes(long noteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (notesBL.TrashChange(userId, noteID))
                {
                    return this.Ok(new { Success = true, message = "Trash changed successfully",response = noteID });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        public IActionResult UploadImage(long noteID, IFormFile image)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (notesBL.UploadImage(userId, noteID, image))
                {
                    return this.Ok(new { Success = true, message = "Image uploaded successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access is denied" });
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
