using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Models
{
    public class ProjectFavourite
    {
        public int ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectDesc { get; set; }
        public string ProjectManager { get; set; }
        public string ProjectStatusDesc { get; set; }
    }
}
