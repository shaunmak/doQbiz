using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMS.Models;
using DMS.Models.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DMS.Controllers
{
    public class HomeController : Controller
    {

        ProjectFavouriteDataAccessLayer objProjectFavouriteList = new ProjectFavouriteDataAccessLayer();

        // GET: /<controller>/
        public IActionResult Index()
        {

            // Project Dropdown
            ProjectDropdownDataAccessLayer objProjectDDL = new ProjectDropdownDataAccessLayer();
            IEnumerable<ProjectDropdown> lstProjectDDL = objProjectDDL.GetProjectDropdownList();
            List<ProjectDropdown> lpdd = new List<ProjectDropdown>();
            lpdd = lstProjectDDL.ToList();
            ViewBag.ProjectDropdownItems = lpdd;


            List<ProjectFavourite> lstProjectFavouriteUserList = new List<ProjectFavourite>();
            lstProjectFavouriteUserList = objProjectFavouriteList.ProjectFavouriteUserListing().ToList();

            ViewBag.PageTitle = "Project Favourite List";
            ViewBag.DocumentBlurb = "Access all your favourite projects here. To add a new project to the list go to the Project listing page by selecting the project from the dropdown and pressing the Select button.";

            return View(lstProjectFavouriteUserList);

        }

        ProjectListingDataAccessLayer objProjectListing = new ProjectListingDataAccessLayer();

        [HttpGet]
        public IActionResult ProjectListingAll()
        {
            List<ProjectListing> projectListing = new List<ProjectListing>();
            projectListing = objProjectListing.getProjectListingAll().ToList();
            return View(projectListing);
        }


        [HttpGet]
        public IActionResult ProjectListing(int ProjectID)
        {
            List<ProjectListing> projectListing = new List<ProjectListing>();
            projectListing = objProjectListing.getProjectListing(ProjectID).ToList();
            return View(projectListing);
        }
    }
}
