using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DMS.Models;

namespace DMS.Controllers
{
    public class DocumentFavouriteController : Controller
    {

        DocUserListDataAccessLayer objDocUserList = new DocUserListDataAccessLayer();

        // GET: /<controller>/
        public IActionResult Index() {
            // string strConnectionString = _iconfiguration.GetValue<string>("Data:ConnectionString");

            List<DocUserList> lstDocUserList = new List<DocUserList>();
            lstDocUserList = objDocUserList.DocUserListing().ToList();

            ViewBag.PageTitle = "Document List";
            ViewBag.DocumentBlurb = "Access all your company documents in one secure location. Your recent documents are listed for your convenience, but you can search the entire database.";

            return View (lstDocUserList);
        }


        ProjectListingDataAccessLayer objprojectListing = new ProjectListingDataAccessLayer();

        public IActionResult ProjectListingAll() {

            List<ProjectListing> lstProjectListing = new List<ProjectListing>();

            lstProjectListing = objprojectListing.getProjectListingAll().ToList();


            return View(lstProjectListing);

        }

    }
}
