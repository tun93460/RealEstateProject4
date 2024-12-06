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

			// Passing utilities and amenities to the view
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



        private List<Amenity> GetAmenities(int homeID)
        {
            List<Amenity> amenities = hda.GetAmenitiesByHomeID(homeID);

            return amenities;
        }

		private List<Amenity> GetUtilities(int homeID)
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
            string homeJson = TempData["Home"]?.ToString();
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
        public IActionResult SaveHome(Home home)
        {
            string homeJson = TempData["Home"]?.ToString();
            Home existingHome;

            if (!string.IsNullOrEmpty(homeJson))
            {
                existingHome = JsonConvert.DeserializeObject<Home>(homeJson);
            }
            else
            {
                existingHome = home;
            }

            if (ModelState.IsValid)
            {
                int totalSize = 0;
                int totalBedrooms = 0;
                int totalBathrooms = 0;

                foreach (var room in existingHome.Rooms)
                {
                    int roomSize = Convert.ToInt32(room.RoomLength) * Convert.ToInt32(room.RoomWidth);
                    totalSize += roomSize;

                    if (room.RoomType.Equals("Bedroom", StringComparison.OrdinalIgnoreCase))
                    {
                        totalBedrooms++;
                    }
                    else if (room.RoomType.Equals("Bathroom", StringComparison.OrdinalIgnoreCase))
                    {
                        totalBathrooms++;
                    }
                }

                existingHome.Size = totalSize;
                existingHome.Bedrooms = totalBedrooms;
                existingHome.Bathrooms = totalBathrooms;

                int addressId = hda.CreateAddress(
                    existingHome.Address.City,
                    existingHome.Address.State,
                    existingHome.Address.Street,
                    Convert.ToInt32(existingHome.Address.Zip)
                );

                if (addressId <= 0)
                {
                    Debug.WriteLine("Failed to create Address. Aborting Home creation.");
                    return RedirectToAction("Index");
                }

                int homeId = hda.CreateHome(
                    existingHome.PropertyType,
                    Convert.ToInt32(existingHome.Price),
                    Convert.ToInt32(existingHome.Size),
                    existingHome.Bedrooms,
                    Convert.ToString(existingHome.Bathrooms),
                    Convert.ToString(existingHome.DateEntered),
                    existingHome.HvacInfo,
                    Convert.ToString(existingHome.YearBuilt),
                    existingHome.GarageType,
                    existingHome.HomeDesc,
                    Convert.ToInt32(existingHome.Status),
                    addressId
                );

                if (homeId <= 0)
                {
                    Debug.WriteLine("Failed to create Home. Aborting Home creation.");
                    return RedirectToAction("Index");
                }

                foreach (var room in existingHome.Rooms)
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

                foreach (var image in existingHome.HomeImages)
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
            }

            Debug.WriteLine("ModelState is invalid. Home creation aborted.");
            return View("Create", home);
        }





    }
}
