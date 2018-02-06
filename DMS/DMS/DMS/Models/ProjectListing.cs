using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Models
{
    public class ProjectListing
    {

        public int ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string DocumentType { get; set; }
        public int DocumentID { get; set; } 
        public string DocFieldTag { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentTitle { get; set; }
        public bool IsCheckedOut { get; set; }
        public string CheckOutBy { get; set; }
        public string DocumentStatus { get; set; }
        public string CurrentRev { get; set; }
        public string Originator { get; set; }
        public string FileLocation { get; set; }
    }
}
