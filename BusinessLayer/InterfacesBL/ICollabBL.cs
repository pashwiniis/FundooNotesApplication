using CommanLayer.Models1;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.InterfacesBL
{
    public interface ICollabBL
    {
        public bool AddCollaborator(CollabaoratorModel collabaoratorModel);
        public IEnumerable<Collaborator> GetCollaboratorsByID(long userID, long noteID);
        public bool RemoveCollaborator(long userID, long noteID, string collabEmail);
    }
}
