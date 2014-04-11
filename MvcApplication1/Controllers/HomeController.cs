using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(string searchString, string currentFilter, string sortOrder, int? page, string msg="")
        {
            ViewBag.Message = msg;
            OrganizationRepository orgRep = new OrganizationRepository();

            IEnumerable<Organization> organizations = orgRep.GetOrganization(sortOrder, searchString);

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            // Store current sort filter parameter.
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = searchString;

            // Provide toggle option for name sort.
            if (String.IsNullOrEmpty(sortOrder))
                ViewBag.NameSortParm = OrganizationRepository.NAME_DESC;
            else
                ViewBag.NameSortParm = OrganizationRepository.NAME;

            // Provide toggle  optionfor date sort.
            if (sortOrder == OrganizationRepository.COUNTRY)
                ViewBag.CountrySortParm = OrganizationRepository.COUNTRY_DESC;
            else
                ViewBag.CountrySortParm = OrganizationRepository.COUNTRY;

            if (sortOrder == OrganizationRepository.CITY)
                ViewBag.CitySortParm = OrganizationRepository.CITY_DESC;
            else
                ViewBag.CitySortParm = OrganizationRepository.CITY;

            if (sortOrder == OrganizationRepository.PROVINCE)
                ViewBag.ProvinceSortParm = OrganizationRepository.PROVINCE_DESC;
            else
                ViewBag.ProvinceSortParm = OrganizationRepository.PROVINCE;


            const int PAGE_SIZE = 6;
            int pageNumber = (page ?? 1);

            // Truncate results to fit in the view provided.
            organizations = organizations.ToPagedList(pageNumber, PAGE_SIZE);


            return View(organizations);
        }

        [HttpGet]
        public ActionResult Login(string msg = "")
        {
            ViewBag.Msg = msg;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            UserRepository user = new UserRepository();
            var log = user.CheckLogin(username, password);
            if (log != null)
            {
                return RedirectToAction("Add");
            }
            else 
            {
                return RedirectToAction("Login", "Home", new {msg= "Your log in or password was not found"});
            }
        }

        public ActionResult Logout()
        {
            UserRepository user = new UserRepository();
            user.Logout();
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string Name, string Contact_fName, string Contact_lName, string Phone, string Street_Address, string City, string Province, string Country, string Postal_Code, float latitude, float longitude)
        {
            OrganizationRepository orgRep = new OrganizationRepository();            
            string message = orgRep.AddListing(Name, Contact_fName, Contact_lName, Phone, Street_Address, City, Province, Country, Postal_Code, latitude, longitude);
            return RedirectToAction("Index", "Home", new { msg = message });
        }

        public ActionResult Remove(int id)
        {
            OrganizationRepository org = new OrganizationRepository();
            string message = org.RemoveListing(id);
            return (RedirectToAction("Index", new { msg = message }));
        }

        public ActionResult ViewMap(int id)
        {
            OrganizationRepository org = new OrganizationRepository();
            return View(org.ViewListing(id));
        }
    }
}
