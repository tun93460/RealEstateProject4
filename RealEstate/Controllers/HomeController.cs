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

        public IActionResult Create()
        {
            var home = new Home
            {
                Address = new Address(),
                Rooms = new List<Room>(),
                HomeImages = new List<HomeImage>()
            };
            return View(home);
        }


        public IActionResult Offer()
        {
            return View("Offer");
        }

        public IActionResult Showing()
        {
            return View("Showing");
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
            ViewData["Title"] = "Viewing Home - ID:" + id;
            Home home = hda.GetHomeByID(id);

            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }


        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Editing Home - ID:" + id;
            Home home = hda.GetHomeByID(id);

            if (home == null)
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

                ViewData["Message"] = "Home has been updated.";

                return RedirectToAction("Index", "Home");
            }
            return View(home);
        }

        [HttpPost]
        public IActionResult AddRoom(Home home, string roomType, string roomDescription, int roomLength, int roomWidth)
        {
            if (home.Rooms == null)
            {
                home.Rooms = new List<Room>();
            }

            home.Rooms.Add(new Room
            {
                RoomType = roomType,
                RoomDescription = roomDescription,
                RoomLength = roomLength,
                RoomWidth = roomWidth
            });
            return View("Create", home);
        }


        [HttpPost]
        public IActionResult AddImage(Home home, string imageCaption)
        {
            if (home.HomeImages == null)
            {
                home.HomeImages = new List<HomeImage>();
            }

            if (home.ImageFile != null)
            {
                using (var stream = new MemoryStream())
                {
                    home.ImageFile.CopyTo(stream);
                    home.HomeImages.Add(new HomeImage
                    {
                        ImageCaption = imageCaption,
                        ImageData = stream.ToArray()
                    });
                }
            }
            return View("Create", home);
        }


        [HttpPost]
        public IActionResult SaveHome(Home home)
        {
            return RedirectToAction("Create");
        }
    }
}
