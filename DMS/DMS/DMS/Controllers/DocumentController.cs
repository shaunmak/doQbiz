using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DMS.Models;
using DMS.Models.ViewModel;

using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DMS.Controllers
{
    public class DocumentController : Controller
    {

        DocumentDataAccessLayer objDocument = new DocumentDataAccessLayer();

        // GET: /<controller>/

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddDocument()
        {
            ViewBag.PageTitle = "Document Add";
            ViewBag.DocumentBlurb = "Use this screen to add a new document, once the document has been added you will be taken to the edit screen to fill out any details required.";

            // Project Dropdown
            ProjectDropdownDataAccessLayer objProjectDDL = new ProjectDropdownDataAccessLayer();
            IEnumerable<ProjectDropdown> lstProjectDDL = objProjectDDL.GetProjectDropdownList();
            List<ProjectDropdown> lpdd = new List<ProjectDropdown>();
            lpdd = lstProjectDDL.ToList();
            ViewBag.ProjectDropdownItems = lpdd;


            // DocumentTypeID
            DocumentTypeDropdownDataAccessLayer objDocumentTypeDDL = new DocumentTypeDropdownDataAccessLayer();
            IEnumerable<DocumentTypeDropdown> lstDocumentTypeDDL = objDocumentTypeDDL.GetDocumentTypeDropdownList();
            List<DocumentTypeDropdown> ldtdd = new List<DocumentTypeDropdown>();
            ldtdd = lstDocumentTypeDDL.ToList();
            ViewBag.DocumentTypeDropdownItems = ldtdd;

            //DocumentStatusID
            DocumentStatusDropdownDataAccessLayer objDocumentStatusDDL = new DocumentStatusDropdownDataAccessLayer();
            IEnumerable<DocumentStatusDropdown> lstDocumentStatusDDL = objDocumentStatusDDL.GetDocumentStatusDropdownList();
            List<DocumentStatusDropdown> ldsdd = new List<DocumentStatusDropdown>();
            ldsdd = lstDocumentStatusDDL.ToList();
            ViewBag.DocumentStatusDropdownItems = ldsdd;

            var ViewDocListing = new AddDocumentListing();

            return View(ViewDocListing);

        }

        [HttpPost]
        public IActionResult AddDocument([Bind] Document AddDocData)
        {
            if (ModelState.IsValid) {

                
                objDocument.AddDocument(AddDocData);

                Document LoadDocData = new Document();
                LoadDocData = objDocument.GetDocumentData(AddDocData.DocumentID);
                return View("EditDocument", LoadDocData );
            }
            return View();

        }

        [HttpGet]
        public IActionResult EditDocument(int DocumentID)
        {
            Document EditDocData = new Document();
            EditDocData = objDocument.GetDocumentData(DocumentID);
            ViewBag.PageTitle = "Document Detail/Edit";
            ViewBag.DocumentBlurb = "Maintain your document metadata, check out your document so no one can edit it, download your document to work on offline upload your document to save as a new revision.";
            return View(EditDocData);  

        }

        [HttpPost]
        public ViewResult EditDocument(int DocumentID, [Bind]Document EditDocData)
        {

            // TO DO - Add validation and checking at a later date - Gavin 12-Jan-2018
            if (ModelState.IsValid)
            {
                objDocument.EditDocument(EditDocData);
                return View(); 
            }
            return View();
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadDocument(int DocumentID, IFormFile DocFileName)
        {
            string strFileDestinationPath;
            string strNewFilename;

            int intDocumentID = 137;

            // To Do:
            // Someone pressed the upload button and there are no files selected.
            // What if someone already has the document open - i.e they've gone on the file server and are editing it outside of the application.

            ProjectDataAccessLayer _pdal = new ProjectDataAccessLayer();

            Document FileDoc = new Document();
            FileDoc = objDocument.GetDocumentData(intDocumentID);

            // TO DO - Add validation and checking at a later date - Gavin 12-Jan-2018
            if (ModelState.IsValid)
            {
                strFileDestinationPath = _pdal.GetFileLocation(FileDoc.ProjectID);
                // Get the filename - entered by the user

                string ext = Path.GetExtension(DocFileName.FileName.ToString());

                // Get the filename - as expected by the database
                strNewFilename = strFileDestinationPath + "\\" + FileDoc.DocumentNo + ".R" + FileDoc.CurrentRev + ext;

                // Get the destination file path.
                using (var stream = new FileStream(strNewFilename, FileMode.Create))
                {
                    // Note that any matching documents in the directory will be overwritten automatically - Gav 30-Jan-2018
                    await DocFileName.CopyToAsync(stream);
                    stream.Close();
    
                }

                // Grab the filename and update the document record with it.
                objDocument.FileUpload(DocumentID, FileDoc.DocumentNo + ".R" + FileDoc.CurrentRev + ext);
            }

            //To Do Needs a bit of work - should open the edit page and fill out the fields - all it does at the moment is open the form without making any changes.
            Document EditDocData = new Document();
            EditDocData = objDocument.GetDocumentData(DocumentID);
            return RedirectToAction("EditDocument");
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAttachment(int DocumentID, IFormFile AttachmentFileName)
        {
            string strFileDestinationPath;
            string strNewFilename;

            ProjectDataAccessLayer _pdal = new ProjectDataAccessLayer();

            Document FileDoc = new Document();
            FileDoc = objDocument.GetDocumentData(DocumentID);

            // TO DO - Add validation and checking at a later date - Gavin 12-Jan-2018
            if (ModelState.IsValid)
            {
                strFileDestinationPath = _pdal.GetFileLocation(FileDoc.ProjectID);
                // Get the filename - entered by the user

                string ext = Path.GetExtension(AttachmentFileName.FileName.ToString());

                // Get the filename - as expected by the database
                strNewFilename = strFileDestinationPath + "\\" + FileDoc.DocumentNo + ".R" + FileDoc.CurrentRev + "-attachment" + ext;
                // Get the destination file path.

                using (var stream = new FileStream(strNewFilename, FileMode.Create))
                {
                    await AttachmentFileName.CopyToAsync(stream);

                }

                // Grab the Attachmentname and update the document record with it.
                objDocument.AttachmentUpload(DocumentID, FileDoc.DocumentNo + ".R" + FileDoc.CurrentRev + "-attachment" + ext);
               
            }

            //To Do Needs a bit of work - should open the edit page and fill out the fields - all it does at the moment is open the form without making any changes.
            Document EditDocData = new Document();
            EditDocData = objDocument.GetDocumentData(DocumentID);
            return RedirectToAction("EditDocument");
        }


        // Download file should take the source path and filename from the 
        public async Task<IActionResult> DownloadDocument(int DocumentID)
        {
            Document FileDoc = new Document();
            FileDoc = objDocument.GetDocumentData(DocumentID);

            ProjectDataAccessLayer _pdal = new ProjectDataAccessLayer();
            string strSourcePath = _pdal.GetFileLocation(FileDoc.ProjectID);

            string strFileDestinationPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string strFileSourceName = strSourcePath + "\\" + FileDoc.DocFileName;

            var memory = new MemoryStream();

            using (var stream = new FileStream(strFileSourceName, FileMode.Open))
            {
                await stream.CopyToAsync(memory);

            }
            memory.Position = 0;

            //return File(memory, FileDoc.GetContentType(strFileSourceName), strFileDestinationPath + "\\" + FileDoc.DocFileName);
            return File(memory, FileDoc.GetContentType(strFileSourceName),  FileDoc.DocFileName);
        }

    }
}
