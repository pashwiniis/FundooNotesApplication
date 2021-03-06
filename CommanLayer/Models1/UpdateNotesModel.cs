using System;
using System.Collections.Generic;
using System.Text;

namespace CommanLayer.Models1
{
    public class UpdateNotesModel
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime Remainder { get; set; }


        public string Color { get; set; }

        public string Image { get; set; }

        public bool IsArchive { get; set; }

        public bool IsPin { get; set; }

        public bool IsTrash { get; set; }
        public DateTime? Createat { get; set; }
        public DateTime? Modifiedat { get; set; }
    }
}
