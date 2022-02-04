using BusinessLayer.InterfacesBL;
using CommanLayer.Models1;
using RepositoryLayer.Entites;
using RepositoryLayer.InterfacesRL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services1
{
    public class CollabBL : ICollabBL
    {
        ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
        public bool AddCollaborator(CollabaoratorModel collabaoratorModel)
        {
            try
            {
                return collabRL.AddCollaborator(collabaoratorModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Collaborator> GetCollaboratorsByID(long userID, long noteID)
        {
            try
            {
                return collabRL.GetCollaboratorsByID(userID, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool RemoveCollaborator(long userID, long noteID, string collabEmail)
        {
            try
            {
                return collabRL.RemoveCollaborator(userID, noteID, collabEmail);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
