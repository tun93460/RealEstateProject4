using Microsoft.AspNetCore.Mvc;
using Project4.Models;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Project4.Models.Utilities;
using System.Reflection;

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
			var home = new HomeCreateViewModel
			{
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
        public IActionResult AddRoom(HomeCreateViewModel home)
        {

            object homeJson = JsonConvert.SerializeObject(home);
            HomeCreateViewModel existingHome;


            if (!string.IsNullOrEmpty(Convert.ToString(homeJson)))
            {
                existingHome = JsonConvert.DeserializeObject<HomeCreateViewModel>(Convert.ToString(homeJson));
            }
            else
            {
                existingHome = home; 
            }

            TempData["Home"] = JsonConvert.SerializeObject(existingHome);

            List<Utility> utilities = hda.GetUtilities();
            List<Amenity> amenities = hda.GetAmenities();

            ViewBag.Utilities = utilities;
            ViewBag.Amenities = amenities;

            if (home.Rooms == null)
                home.Rooms = new List<Room>();


            home.Rooms.Add(new Room
            {
                RoomType = home.RoomType,
                RoomDescription = home.RoomDescription,
                RoomLength = home.RoomLength,
                RoomWidth = home.RoomWidth
            });

            return View("Create", existingHome);
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

            object homeJson = JsonConvert.SerializeObject(home);
            HomeCreateViewModel existingHome;

            if (!string.IsNullOrEmpty(Convert.ToString(homeJson)))
            {
                existingHome = JsonConvert.DeserializeObject<HomeCreateViewModel>(Convert.ToString(homeJson));
            }
            else
            {
                existingHome = home;
            }

            int totalSize = 0;
            int totalBedrooms = 0;
            int totalBathrooms = 0;

            foreach (object room in existingHome.Rooms)
            {
                int roomSize = Convert.ToInt32(((Room)room).RoomLength) * Convert.ToInt32(((Room)room).RoomWidth);
                totalSize += roomSize;

                if (((Room)room).RoomType.Equals("Bedroom", StringComparison.OrdinalIgnoreCase))
                {
                    totalBedrooms++;
                }
                else if (((Room)room).RoomType.Equals("Bathroom", StringComparison.OrdinalIgnoreCase))
                {
                    totalBathrooms++;
                }
            }

            existingHome.Size = totalSize;
            existingHome.Bedrooms = totalBedrooms;
            existingHome.Bathrooms = totalBathrooms;

            int addressId = hda.CreateAddress(
                existingHome.City.ToString(),
                existingHome.State.ToString(),
                existingHome.Street.ToString(),
                Convert.ToInt32(existingHome.Zip)
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

            hda.LinkHomeWithBroker(homeId, 3);

            foreach (object room in existingHome.Rooms)
            {
                int roomId = hda.CreateRoom(
                    Convert.ToString(((Room)room).RoomType),
                    Convert.ToString(((Room)room).RoomDescription),
                    Convert.ToInt32(((Room)room).RoomWidth),
                    Convert.ToInt32(((Room)room).RoomLength)
                );

                if (roomId > 0)
                {
                    hda.LinkHomeWithRoom(homeId, roomId);
                }
                else
                {
                    Debug.WriteLine($"Failed to create Room: {((Room)room).RoomType}. Continuing with remaining items.");
                }
            }

            foreach (object amenity in existingHome.Amenities)
            {
                int amenityId = hda.CreateAmenities(Convert.ToString(((Amenity)amenity).AmenityType));

                if (amenityId > 0)
                {
                    hda.LinkHomeWithAmenities(homeId, amenityId);
                }
                else
                {
                    Debug.WriteLine($"Failed to create Amenity: {((Amenity)amenity).AmenityType}. Continuing with remaining items.");
                }
            }

            foreach (object utility in existingHome.Utilities)
            {
                int utilityId = hda.CreateUtilities(Convert.ToString(((Utility)utility).UtilityType));

                if (utilityId > 0)
                {
                    hda.LinkHomeWithUtilities(homeId, utilityId);
                }
                else
                {
                    Debug.WriteLine($"Failed to create Utility: {((Utility)utility).UtilityType}. Continuing with remaining items.");
                }
            }

            /*foreach (object image in existingHome.ExistingHome.HomeImages)
            {
                int imageId = hda.CreateImage(
                    Convert.ToString(((HomeImage)image).ImageData),
                    Convert.ToString(((HomeImage)image).ImageCaption)
                );

                if (imageId > 0)
                {
                    hda.LinkHomeWithImage(homeId, imageId);
                }
                else
                {
                    Debug.WriteLine($"Failed to create Image: {((HomeImage)image).ImageCaption}. Continuing with remaining items.");
                }
            }*/

            TempData.Remove("Home");

            ViewData["Message"] = "Home has been saved!";
            Debug.WriteLine($"Home with ID {homeId} successfully created.");
            return RedirectToAction("Index", "Home");
        }

    }
}
