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

			List<Utility> utilities = hda.GetUtilities(); 
			List<Amenity> amenities = hda.GetAmenities();


			ViewBag.Utilities = utilities;
			ViewBag.Amenities = amenities;

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

			List<Utility> utilities = hda.GetUtilitiesByHomeID(id);
			List<Amenity> amenities = hda.GetAmenitiesByHomeID(id);


			ViewBag.Utilities = utilities;
			ViewBag.Amenities = amenities;

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
        public IActionResult AddRoom(string roomType, string roomDescription, double roomLength, double roomWidth,
                                      List<int> selectedUtilities, List<int> selectedAmenities)
        {
            string homeJson = TempData["Home"]?.ToString();
            Home home;

            if (!string.IsNullOrEmpty(homeJson))
            {
                home = JsonConvert.DeserializeObject<Home>(homeJson);
            }
            else
            {
                home = new Home();
            }

            home.SelectedUtilities = selectedUtilities ?? new List<int>();
            home.SelectedAmenities = selectedAmenities ?? new List<int>();

            if (selectedUtilities != null)
            {
                foreach (var utilityId in selectedUtilities)
                {
                    var utility = hda.GetUtilities().FirstOrDefault(u => u.UtilityID == utilityId);
                    if (utility != null)
                    {
                        home.AddUtility(utility);
                    }
                }
            }

            if (selectedAmenities != null)
            {
                foreach (var amenityId in selectedAmenities)
                {
                    var amenity = hda.GetAmenities().FirstOrDefault(a => a.AmenityID == amenityId);
                    if (amenity != null)
                    {
                        home.AddAmenity(amenity);
                    }
                }
            }

            home.Rooms.Add(new Room
            {
                RoomType = roomType,
                RoomDescription = roomDescription,
                RoomLength = roomLength,
                RoomWidth = roomWidth
            });

            TempData["Home"] = JsonConvert.SerializeObject(home);

            List<Utility> utilities = hda.GetUtilities();
            List<Amenity> amenities = hda.GetAmenities();

            ViewBag.Utilities = utilities;
            ViewBag.Amenities = amenities;

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
        public IActionResult SaveHome(HomeCreateViewModel home)
        {

            if (home == null)
            {
                Debug.WriteLine("Home object is null.");
                return View("Index");
            }
            //string homeJson = TempData["Home"].ToString();
            HomeCreateViewModel existingHome;
            string homeJson = JsonConvert.SerializeObject(home);

            if (!string.IsNullOrEmpty(homeJson))
            {
                existingHome = JsonConvert.DeserializeObject<HomeCreateViewModel>(homeJson);
            }
            else
            {
                existingHome = home;
            }



            int totalSize = 0;
            int totalBedrooms = 0;
            int totalBathrooms = 0;

            //foreach (var room in existingHome.Home.Rooms)
            //{
            //    int roomSize = Convert.ToInt32(room.RoomLength) * Convert.ToInt32(room.RoomWidth);
            //    totalSize += roomSize;
            //
            //    if (room.RoomType.Equals("Bedroom", StringComparison.OrdinalIgnoreCase))
            //    {
            //        totalBedrooms++;
            //    }
            //    else if (room.RoomType.Equals("Bathroom", StringComparison.OrdinalIgnoreCase))
            //    {
            //        totalBathrooms++;
            //    }
            //}

          /*  existingHome.Home.Size = totalSize;
            existingHome.Home.Bedrooms = totalBedrooms;
            existingHome.Home.Bathrooms = totalBathrooms; */


            /*int addressId = hda.CreateAddress(
                existingHome.ExistingHome.Address.City.ToString(),
                existingHome.ExistingHome.Address.State.ToString(),
                existingHome.ExistingHome.Address.Street.ToString(),
                Convert.ToInt32(existingHome.ExistingHome.Address.Zip)
            );*/

            /*if (addressId <= 0)
            {
                Debug.WriteLine("Failed to create Address. Aborting Home creation.");
                return RedirectToAction("Index");
            }*/

            int homeId = hda.CreateHome(
                existingHome.ExistingHome.PropertyType,
                Convert.ToInt32(existingHome.ExistingHome.Price),
                Convert.ToInt32(existingHome.ExistingHome.Size),
                existingHome.ExistingHome.Bedrooms,
                Convert.ToString(existingHome.ExistingHome.Bathrooms),
                Convert.ToString(existingHome.ExistingHome.DateEntered),
                existingHome.ExistingHome.HvacInfo,
                Convert.ToString(existingHome.ExistingHome.YearBuilt),
                existingHome.ExistingHome.GarageType,
                existingHome.ExistingHome.HomeDesc,
                Convert.ToInt32(existingHome.ExistingHome.Status),
                3
            );

            if (homeId <= 0)
            {
                Debug.WriteLine("Failed to create Home. Aborting Home creation.");
                return RedirectToAction("Index");
            }

            hda.LinkHomeWithBroker(homeId, 3);

            foreach (var room in existingHome.ExistingHome.Rooms)
            {
                int roomId = hda.CreateRoom(
                    room.RoomType,
                    room.RoomDescription,
                    Convert.ToInt32(room.RoomWidth),
                    Convert.ToInt32(room.RoomLength)
                );

                if (roomId > 0)
                {
                    hda.LinkHomeWithRoom(homeId, roomId);
                }
                else
                {
                    Debug.WriteLine($"Failed to create Room: {room.RoomType}. Continuing with remaining items.");
                }
            }

            foreach (var amenity in existingHome.Amenities)
            {
                int amenityId = hda.CreateAmenities(amenity.AmenityType);

                if (amenityId > 0)
                {
                    hda.LinkHomeWithAmenities(homeId, amenityId);
                }
                else
                {
                    Debug.WriteLine($"Failed to create Amenity: {amenity.AmenityType}. Continuing with remaining items.");
                }
            }

            foreach (var utility in existingHome.Utilities)
            {
                int utilityId = hda.CreateUtilities(utility.UtilityType);

                if (utilityId > 0)
                {
                    hda.LinkHomeWithUtilities(homeId, utilityId);
                }
                else
                {
                    Debug.WriteLine($"Failed to create Utility: {utility.UtilityType}. Continuing with remaining items.");
                }
            }

            foreach (var image in existingHome.ExistingHome.HomeImages)
            {
                int imageId = hda.CreateImage(
                    image.ImageData,
                    image.ImageCaption
                );

                if (imageId > 0)
                {
                    hda.LinkHomeWithImage(homeId, imageId);
                }
                else
                {
                    Debug.WriteLine($"Failed to create Image: {image.ImageCaption}. Continuing with remaining items.");
                }
            }

            TempData.Remove("Home");

            ViewData["Message"] = "Home has been saved!";
            Debug.WriteLine($"Home with ID {homeId} successfully created.");
            return RedirectToAction("Index", "Home");


            Debug.WriteLine("ModelState is invalid. Home creation aborted.");
            return View("Index", home);
        }
    }
}
