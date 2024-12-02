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
        public IActionResult SearchHomes(string city, string state, string zip, string propertyType, int minBedrooms, int minBathrooms, double minPrice, double maxPrice, double minHomeSize)
        {
                List<Home> homes = hda.SearchHomes(city, state, zip, propertyType, minBedrooms, minBathrooms, minPrice, maxPrice, minHomeSize);
                

                return View("Index", homes);
            }



        private List<Amenity> GetAmenities(int homeID)
        {
            DataSet dsAmenities = hda.GetAmenitiesByHomeID(homeID);

            List<Amenity> amenities = new List<Amenity>();
            if (dsAmenities.Tables.Count > 0)
            {
                foreach (DataRow row in dsAmenities.Tables[0].Rows)
                {
                    amenities.Add(new Amenity
                    {
                        AmenitiesDescription = row["amenitiesDescription"].ToString(),
                        AmenitiesID = Convert.ToInt32(row["amenitiesID"]),
                        AmenitiesName = row["amenitiesType"].ToString()
                    });
                }
            }

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
