using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMS.Models;

namespace DMS.Models.ViewModel
{
    public class AddDocumentListing
    {
        // Project Dropdown
        public int ProjectID { get; set; }
        public string ProjectNo { get; set; }

        // Document Status Dropdown
        public int DocumentStatusID { get; set; }
        public string StatusDesc { get; set; }

        // Document Type Dropdown
        public int DocumentTypeID { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeDesc { get; set; }

        // Other document data
        public int DocumentID { get; set; }
        public string DocumentTitle { get; set; }
        public string CurrentRev { get; set; }
        public string UserID { get; set; }

        //public ProjectDropdown ProjectDropDown { get; set; }

        //public DocumentTypeDropdown DocumentTypeDropdown { get; set; }

        //public DocumentStatusDropdown DocumentStatusDropdown { get; set; }

        //public IEnumerable<ProjectDropdown> GetDropdownList { get; set; }
    }
}
