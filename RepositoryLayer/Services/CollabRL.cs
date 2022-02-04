using CommanLayer.Models1;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entites;
using RepositoryLayer.InterfacesRL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollabRL : ICollabRL
    {
        Context context;
        private readonly IConfiguration configuration;
        /// <summary>
        /// defines the collaborator constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="config"></param>
        public CollabRL(Context context, IConfiguration config)
        {
            this.context = context;//appcontext to for api
            this.configuration = config;//for startup file instance
        }
        /// <summary>
        /// it definies the to adding the collaborator in notes
        /// </summary>
        /// <param name="collabaoratorModel"></param>
        /// <returns true></returns>
        ///  <returns false ></returns>
        public bool AddCollaborator(CollabaoratorModel collabaoratorModel)
        {
            try
            {
                Collaborator collaborator = new Collaborator();
                Notes notes = context.Notes.Where(e => e.NoteId == collabaoratorModel.NoteID && e.Id == collabaoratorModel.Id).FirstOrDefault();
                if (notes != null)
                {
                    collaborator.NoteID = collabaoratorModel.NoteID;
                    collaborator.CollabEmail = collabaoratorModel.CollabEmail;
                    collaborator.Id = collabaoratorModel.Id;
                    context.Collaborators.Add(collaborator);
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
        /// <summary>
        /// it defines the method to get all collaborator by using note id and user id
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="noteID"></param>
        /// <returns result></returns>
        ///  <returns false ></returns>
        public IEnumerable<Collaborator> GetCollaboratorsByID(long userID, long noteID)
        {
            try
            {
                var result = context.Collaborators.Where(e => e.Id == userID && e.NoteID == noteID).ToList();
                if (result != null)
                    return result;
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// it defines the method to delete the parcular collaborator
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="noteID"></param>
        /// <param name="collabEmail"></param>
        /// <returns true></returns>
        ///  <returns false ></returns>
        public bool RemoveCollaborator(long userID, long noteID, string collabEmail)
        {
            try
            {
                var collaborator = context.Collaborators.Where(e => e.CollabEmail == collabEmail && e.NoteID == noteID).FirstOrDefault();
                if (collaborator != null)
                {
                    context.Collaborators.Remove(collaborator);
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

    }
}
