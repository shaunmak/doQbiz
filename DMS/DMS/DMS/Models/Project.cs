using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectDesc { get; set; }
        public string ProjectManager { get; set; }
        public int ProjectStatusID { get; set; }
        public string ProjectStatusDesc { get; set; }
        public string FileLocation { get; set; }
        public string URLFileLocation { get; set; }
        public bool IncludeInDD { get; set; }
        public bool IsDeleted { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
