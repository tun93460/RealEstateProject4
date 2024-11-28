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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeDataAccess homeDataAccess = new HomeDataAccess();
            var dataSet = homeDataAccess.GetAllHomes(null, null, 0, 0, 0, 0);

            List<Home> homes = new List<Home>();

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                int homeID = Convert.ToInt32(row["HomeID"]);
                Address homeAddress = new Address(
                    homeID,
                    row["City"].ToString(),
                    row["State"].ToString(),
                    row["Street"].ToString(),
                    row["ZipCode"].ToString()
                );

                List<Amenity> homeAmenities = GetAmenities(homeID);

                homes.Add(new Home
                {
                    HomeID = homeID,
                    Address = homeAddress,
                    PropertyType = row["PropertyType"].ToString(),
                    Price = Convert.ToDecimal(row["AskingPrice"]),
                    Size = Convert.ToInt32(row["HomeSizeTotal"]),
                    Bedrooms = Convert.ToInt32(row["Bedrooms"]),
                    Bathrooms = Convert.ToInt32(row["Bathrooms"]),
                    Amenities = homeAmenities
                });
            }

            return View(homes);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SearchHomes(string location, string propertyType, int? minBedrooms, int? minBathrooms, decimal? minPrice, decimal? maxPrice, string amenities)
        {
            HomeDataAccess homeDataAccess = new HomeDataAccess();
            DataSet dsHomes = homeDataAccess.SearchHomes(location, propertyType, minBedrooms ?? 0, minBathrooms ?? 0, minPrice ?? 0, maxPrice ?? null);
                List<Home> homes = new List<Home>();
                if (dsHomes.Tables.Count > 0)
                {
                    foreach (DataRow row in dsHomes.Tables[0].Rows)
                    {
                        int homeID = Convert.ToInt32(row["HomeID"]);
                        Address homeAddress = new Address(
                            homeID,
                            row["City"].ToString(),
                            row["State"].ToString(),
                            row["Street"].ToString(),
                            row["ZipCode"].ToString()
                        );

                        List<Amenity> homeAmenities = GetAmenities(homeID);

                        homes.Add(new Home
                        {
                            HomeID = homeID,
                            Address = homeAddress,
                            PropertyType = row["PropertyType"].ToString(),
                            Price = Convert.ToDecimal(row["AskingPrice"]),
                            Size = Convert.ToInt32(row["HomeSizeTotal"]),
                            Bedrooms = Convert.ToInt32(row["Bedrooms"]),
                            Bathrooms = Convert.ToInt32(row["Bathrooms"]),
                            Amenities = homeAmenities
                        });
                    }
                }

                return View("Index", homes);
            }



            private List<Amenity> GetAmenities(int homeID)
        {
            HomeDataAccess homeDataAccess = new HomeDataAccess();
            DataSet dsAmenities = homeDataAccess.GetAmenitiesByHomeID(homeID);

            List<Amenity> amenities = new List<Amenity>();
            if (dsAmenities.Tables.Count > 0)
            {
                foreach (DataRow row in dsAmenities.Tables[0].Rows)
                {
                    amenities.Add(new Amenity
                    {
                        AmenityDescription = row["AmenityDescription"].ToString(),
                        HomeID = homeID
                    });
                }
            }

            return amenities;
        }
    }
}
