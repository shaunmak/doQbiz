using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Models
{
    public class DocumentTypeDropdown
    {
        public int DocumentTypeID { get; set;}
        public string DocumentType { get; set; }
        public string DocumentTypeDesc { get; set; }
        public string DocumentDropdownText { get; set; }
    }
}
