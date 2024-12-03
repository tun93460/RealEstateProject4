using Microsoft.AspNetCore.Mvc;
using Project4.Models;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HomeDataAccess hda = new HomeDataAccess();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Home> homes = hda.GetAllHomeIDs();

            return View(homes);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SearchHomes(string location, string propertyType, int minBedrooms, int minBathrooms, double minPrice, double maxPrice, double minHomeSize)
        {
                List<Home> homes = hda.SearchHomes(location, propertyType, minBedrooms, minBathrooms, minPrice, maxPrice, minHomeSize);
                

                return View("Index", homes);
            }



        private List<Amenity> GetAmenities(int homeID)
        {
            List<Amenity> amenities = hda.GetAmenitiesByHomeID(homeID);

            return amenities;
        }

        public IActionResult ViewHome(int id)
        {
            Home home = hda.GetHomeByID(id); 

            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }


        public IActionResult Edit(int id)
        {
            Home home = hda.GetHomeByID(id); 

            if (home==null)
            {
                return NotFound();
            }

            return View(home);
        }

        [HttpPost]
        public IActionResult Edit(Home home)
        {
            if (ModelState.IsValid)
            {
                //updates

                //address

                //utils

                //amenities

                //images

                return RedirectToAction("Index");
            }
            return View(home);
        }
    }
}
