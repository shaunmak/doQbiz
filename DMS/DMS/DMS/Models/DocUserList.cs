using System;
using System.ComponentModel.DataAnnotations;

namespace DMS.Models
{
    public class DocUserList
    {
        public int DocumentID { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentTitle { get; set; }
        public string DocFileName { get; set; }
        public string CurrentRev { get; set; }
        public string DocumentStatus { get; set; }
        public int ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string LastUpdateBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LastUpdate { get; set; }

    }
}
