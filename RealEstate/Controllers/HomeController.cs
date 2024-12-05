using Microsoft.AspNetCore.Mvc;
using Project4.Models;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Project4.Models.Utilities;

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
                Amenities = new List<Amenity>(),
                Utilities = new List<Utility>(),
                HomeImages = new List<HomeImage>()
            };

            TempData["Home"] = JsonConvert.SerializeObject(home);
            TempData.Keep("Home");

            return View(home);
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

            TempData["Home"] = JsonConvert.SerializeObject(home);

            return View(home);
        }

        [HttpPost]
        public IActionResult Edit()
        {
            string homeJson = TempData["Home"].ToString();
            Home home;

            if (!string.IsNullOrEmpty(homeJson))
            {
                home = new Home();
            }
            else
            {
                home = JsonConvert.DeserializeObject<Home>(homeJson);
            }

            if (ModelState.IsValid)
            {
                //updates

                //address

                //utils

                //amenities

                //images

                //remove home data
                TempData.Remove("Home");

                ViewData["Message"] = "Home has been updated.";

                return RedirectToAction("Index", "Home");
            }
            return View(home);
        }

        [HttpPost]
        public IActionResult AddRoom(string roomType, string roomDescription, int roomLength, int roomWidth)
        {
            string homeJson = TempData["Home"].ToString();
            Home home;

            if (!string.IsNullOrEmpty(homeJson))
            {
                home = new Home { Rooms = new List<Room>() };
            }
            else
            {
                home = JsonConvert.DeserializeObject<Home>(homeJson);
            }

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

            TempData["Home"] = JsonConvert.SerializeObject(home);

            return View("Create", home);
        }


        [HttpPost]
        public IActionResult AddImage(string imageCaption, IFormFile imageFile)
        {
            string homeJson = TempData["Home"].ToString();
            Home home;

            if (!string.IsNullOrEmpty(homeJson))
            {
                home = new Home { HomeImages = new List<HomeImage>() };
            }
            else
            {
                home = JsonConvert.DeserializeObject<Home>(homeJson);
            }

            if (home.HomeImages == null)
            {
                home.HomeImages = new List<HomeImage>();
            }

            if (imageFile != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    imageFile.CopyTo(stream);
                    home.HomeImages.Add(new HomeImage
                    {
                        ImageCaption = imageCaption,
                        ImageData = stream.ToArray()
                    });
                }
            }

            TempData["Home"] = JsonConvert.SerializeObject(home);

            return View("Create", home);
        }



        [HttpPost]
        public IActionResult SaveHome()
        {
            string homeJson = TempData["Home"].ToString();
            Home home;

            if (!string.IsNullOrEmpty(homeJson))
            {
                home = new Home();
            }
            else
            {
                home = JsonConvert.DeserializeObject<Home>(homeJson);
            }

            if (ModelState.IsValid)
            {
                //update database

                //clear home data
                TempData.Remove("Home");

                //success message
                //direct to index or my homes
                ViewData["Message"] = "Home has been saved!";
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Create", home);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult SendEmail()
        {
            Email emailObj = new Email();
            string strTo = "tuo76098@temple.edu";
            string? strFrom = "tun93460@temple.edu";
            string strSubject = "Offer Accepted";
            string strMessage = "Congratulations, Your offer has been accepted. Welcome to your new home";

            try
            {
                emailObj.SendMail(strTo, strFrom, strSubject, strMessage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return View("SendEmail");
        }

        public IActionResult CreateHome()
        {
            DataSet utilitiesData = hda.GetUtilities();
            DataSet amenitiesData = hda.GetAmenities();

            List<Utility> utilities = new List<Utility>();
            if (utilitiesData != null && utilitiesData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in utilitiesData.Tables[0].Rows)
                {
                    utilities.Add(new Utility
                    {
                        UtilityID = Convert.ToInt32(row["UtilityID"]),
                        UtilityType = row["UtilityType"].ToString()
                    });
                }
            }

            List<Amenity> amenities = new List<Amenity>();
            if (amenitiesData != null && amenitiesData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in amenitiesData.Tables[0].Rows)
                {
                    amenities.Add(new Amenity
                    {
                        AmenityID = Convert.ToInt32(row["AmenitiesID"]),
                        AmenityType = row["AmenitiesType"].ToString()
                    });
                }
            }

            // Create the view model and pass it to the view
            var model = new HomeCreateViewModel
            {
                Utilities = utilities,
                Amenities = amenities
            };

            return View(model);
        }
    }
}
