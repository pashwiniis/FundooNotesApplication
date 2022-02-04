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
    public class LabelRL : ILabelRL
    {
        /// <summary>
        /// decared the variables
        /// </summary>
        Context context;
        private readonly IConfiguration configuration;
        /// <summary>
        /// defines the lable constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="config"></param>
        public LabelRL(Context context, IConfiguration config)
        {
            this.context = context;//appcontext to for api
            this.configuration = config;//for startup file instance
        }
        /// <summary>
        /// it defines the method for creating a label
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="noteID"></param>
        /// <param name="labelName"></param>
        /// <returns true ></returns>
        ///  <returns false></returns>
        public bool CreateLabel(long userID, long noteID, string labelName)
        {
            try
            {
                Labels labels = new Labels();
                var findlabel = context.Labels.Where(e => e.LabelName == labelName).FirstOrDefault();
                if (findlabel==null)
                {
                    labels.LabelName = labelName;
                    labels.NoteID = noteID;
                    labels.Id = userID;
                    context.Labels.Add(labels);
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
        /// it defines the method to renaming the label name
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="oldLabelName"></param>
        /// <param name="labelName"></param>
        /// <returns true></returns>
        ///  <returns false></returns>
        public bool RenameLabel(long userID, string oldLabelName, string labelName)
        {
            IEnumerable<Labels> labels;
            labels = context.Labels.Where(e => e.Id==userID&&e.LabelName==oldLabelName).ToList();
            if (labels != null)
            {
                foreach (var label in labels)
                {
                    label.LabelName = labelName;
                }
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// it defines the menthod to removing the particular label
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="labelName"></param>
        /// <returns true></returns>
        ///  <returns false></returns>
        public bool RemoveLabel(long userID, string labelName)
        {
            IEnumerable<Labels> labels;
            labels = context.Labels.Where(e => e.Id == userID && e.LabelName == labelName).ToList();
            if (labels != null)
            {
                foreach (var label in labels)
                {
                    context.Labels.Remove(label);
                }
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// it defines the method to get all labels by using note id
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="noteID"></param>
        /// <returns result></returns>
        ///  <returns null ></returns>
        public IEnumerable<Labels> GetLabelsByNoteID(long userID, long noteID)
        {
            try
            {
                var result = context.Labels.Where(e => e.NoteID == noteID && e.Id == userID).ToList();
                if (result!=null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
