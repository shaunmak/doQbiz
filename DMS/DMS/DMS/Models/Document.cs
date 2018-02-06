using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DMS.Models
{
    public class Document
    {
        public int DocumentID { get; set; }
        public int ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeDesc { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentTitle { get; set; }
        public string CurrentRev { get; set; }
        public int DocumentStatusID { get; set; }
        public string DocumentStatus { get; set; }
        public string DocFileLocation { get; set; }
        public string DocFileName { get; set; }
        public string AttachmentFileName { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string SignedOffBy { get; set; }
        public DateTime? SignoffDate { get; set; }
        public bool IsCheckedOut { get; set; }
        public string CheckOutBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CheckOutDate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }

        public string UserID { get; set; }


    public string GetContentType(string path)
    {
        var types = GetMimeTypes();
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return types[ext];
    }

    public Dictionary<string, string> GetMimeTypes()
    {
        return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
    }
    }


}
