using BusinessLayer.InterfacesBL;
using CommanLayer.Models1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundooNotesDuplicate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        ICollabBL collabBL;
        public CollaboratorController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddCollaborator(long noteID, string collabEmail)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                CollabaoratorModel collabaoratorModel = new CollabaoratorModel();
                collabaoratorModel.Id = userId;
                collabaoratorModel.NoteID = noteID;
                collabaoratorModel.CollabEmail = collabEmail;
                if (collabBL.AddCollaborator(collabaoratorModel))
                {
                    return this.Ok(new { Success = true, message = "Collaborator added successfully",response = collabEmail,noteID });
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
        [Authorize]
        [HttpGet]
        public IEnumerable<Collaborator> GetCollaboratorsByID(long noteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                return collabBL.GetCollaboratorsByID(userId, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        public IActionResult RemoveCollaborator(long noteID, string collaboratorEmail)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (collabBL.RemoveCollaborator(userId, noteID, collaboratorEmail))
                {
                    return this.Ok(new { success = "true", message = "Collaborator removed successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = "false", message = "User Access denied" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

