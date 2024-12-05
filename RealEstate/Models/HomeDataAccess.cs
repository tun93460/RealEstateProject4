using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace Project4.Models
{
    public class HomeDataAccess
    {
        private DBConnect dbConnect = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        DataSet ds = new DataSet();
        List<Home> homes = new List<Home>();
        List<Amenity> amenities = new List<Amenity>();
        List<Utility> utilities = new List<Utility>();
        List<Room> rooms = new List<Room>();
        List<HomeImage> images = new List<HomeImage>();
        Home home;
        Amenity amenity;
        Utility utility;
        Room room;
        HomeImage image;


        public Home GetHomeByID(int homeID)
        {

            home = new Home();

            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetHomeByID";

            SqlParameter inputID = new SqlParameter("@HomeID", homeID)
            {
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int
            };
            objCommand.Parameters.Add(inputID);

            ds = dbConnect.GetDataSet(objCommand);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                //populate home fields
                home.HomeID = homeID;
                home.DateEntered = (DateTime)row["dateEntered"];
                home.PropertyType = row["propType"].ToString();
                //calculate home size at end
                home.Bathrooms = Convert.ToInt32(row["numBathrooms"]);
                home.Bedrooms = Convert.ToInt32(row["numBedrooms"]);
                home.HvacInfo = row["hvacInfo"].ToString();
                home.YearBuilt = Convert.ToInt32(row["yearBuilt"]);
                home.GarageType = row["garageType"].ToString();
                home.HomeDesc = row["homeDesc"].ToString();
                home.Price = Convert.ToDouble(row["askingPrice"]);
                home.Status = row["status"].ToString();

                //create address
                home.Address = new Address
                {
                    AddressID = Convert.ToInt32(row["addressID"]),
                    Street = row["street"].ToString(),
                    City = row["city"].ToString(),
                    State = row["state"].ToString(),
                    Zip = row["zip"].ToString()
                };

                //add amenities
                home.Amenities = GetAmenitiesByHomeID(homeID);


                //add utilities
                home.Utilities = GetUtilitiesByHomeID(homeID);

                //add rooms
                home.Rooms = GetRoomsByHomeID(homeID);

                //add Images
                //home.HomeImages = GetImagesByHomeID(homeID);


                //calculate home size based on room dimensions
                foreach (Room room in home.Rooms)
                {
                    home.Size += room.RoomLength * room.RoomWidth;
                }
            }
            objCommand.Parameters.Clear();

            return home;
        }
            

        public List<Home> GetAllHomeIDs()
        {
            SqlCommand cmdSearchHomes = new SqlCommand("GetAllHomeIDs");
            cmdSearchHomes.CommandType = CommandType.StoredProcedure;

            ds = dbConnect.GetDataSetUsingCmdObj(cmdSearchHomes);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in  ds.Tables[0].Rows)
                {
                    home = GetHomeByID(Convert.ToInt32(row["homeID"]));
                    homes.Add(home);
                }
            }

            return homes;
        }

        public List<Home> SearchHomes(string location, string propertyType, int minBedrooms, int minBathrooms, double minPrice, double maxPrice, double minHomeSize)
        {
            SqlCommand cmdSearchHomes = new SqlCommand("SearchHomes");
            cmdSearchHomes.CommandType = CommandType.StoredProcedure;
            cmdSearchHomes.Parameters.Clear();

            if (!string.IsNullOrEmpty(location))
                cmdSearchHomes.Parameters.AddWithValue("@Location", location);
            else
                cmdSearchHomes.Parameters.AddWithValue("@City", DBNull.Value);

            if (!string.IsNullOrEmpty(propertyType))
                cmdSearchHomes.Parameters.AddWithValue("@PropType", propertyType);
            else
                cmdSearchHomes.Parameters.AddWithValue("@PropType", DBNull.Value);

            if (minBedrooms > 0)
                cmdSearchHomes.Parameters.AddWithValue("@MinBeds", minBedrooms);
            else
                cmdSearchHomes.Parameters.AddWithValue("@MinBeds", DBNull.Value);

            if (minBathrooms > 0)
                cmdSearchHomes.Parameters.AddWithValue("@MinBaths", minBathrooms);
            else
                cmdSearchHomes.Parameters.AddWithValue("@MinBaths", DBNull.Value);

            if (minPrice > 0)
                cmdSearchHomes.Parameters.AddWithValue("@MinPrice", minPrice);
            else
                cmdSearchHomes.Parameters.AddWithValue("@MinPrice", DBNull.Value);

            if (maxPrice > 0)
                cmdSearchHomes.Parameters.AddWithValue("@MaxPrice", maxPrice);
            else
                cmdSearchHomes.Parameters.AddWithValue("@MaxPrice", DBNull.Value);

            if (minHomeSize > 0)
                cmdSearchHomes.Parameters.AddWithValue("@MinHomeSize", minHomeSize);
            else
                cmdSearchHomes.Parameters.AddWithValue("@MinHomeSize", DBNull.Value);

            ds = dbConnect.GetDataSetUsingCmdObj(cmdSearchHomes);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    home = GetHomeByID(Convert.ToInt32(row["homeID"]));
                    homes.Add(home);
                }
            }

            cmdSearchHomes.Parameters.Clear();
            return homes;
        }

        public List<Amenity> GetAmenitiesByHomeID(int homeID)
        {
            
            SqlCommand cmdGetAmenities = new SqlCommand("GetAmenitiesByHomeID");
            cmdGetAmenities.CommandType = CommandType.StoredProcedure;
            cmdGetAmenities.Parameters.Clear();
            cmdGetAmenities.Parameters.AddWithValue("@HomeID", homeID);
            
            ds = dbConnect.GetDataSetUsingCmdObj(cmdGetAmenities);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    amenity = new Amenity
                    {
                        AmenitiesID = Convert.ToInt32(row["amenitiesID"]),
                        AmenitiesName = row["AmenitiesType"].ToString()
                    };
                    amenities.Add(amenity);    
                }
            }
            cmdGetAmenities.Parameters.Clear();
            return amenities;
        }

        public List<Utility> GetUtilitiesByHomeID(int homeID)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetUtilitiesByHomeID";
            objCommand.Parameters.AddWithValue("@HomeID", homeID);

            ds = dbConnect.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    utility = new Utility
                    {
                        UtilityID = Convert.ToInt32(row["utilitiesID"]),
                        UtilityType = row["utilitiesType"].ToString()
                    };
                    utilities.Add(utility);
                }
            }
            objCommand.Parameters.Clear();
            return utilities;
        }

        public List<Room> GetRoomsByHomeID(int homeID)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetRoomsByHomeID";
            objCommand.Parameters.AddWithValue("@HomeID", homeID);

            ds = dbConnect.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    room = new Room
                    {
                        RoomID = Convert.ToInt32(row["roomID"]),
                        RoomDescription = row["roomDescription"].ToString(),
                        RoomLength = Convert.ToDouble(row["roomLength"]),
                        RoomWidth = Convert.ToDouble(row["roomWidth"]),
                        RoomType = row["roomType"].ToString()
                    };

                    rooms.Add(room);
                }
            }
            objCommand.Parameters.Clear();
            return rooms;
        }

        public List<HomeImage> GetImagesByHomeID(int homeID)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetImagesByHomeID";
            objCommand.Parameters.AddWithValue("@HomeID", homeID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    image = new HomeImage
                    {
                        ImageID = Convert.ToInt32(row["imageID"]),
                        ImageCaption = row["imageCaption"].ToString(),
                        ImageData = row["imageData"] as Byte[],
                        ImageName = row["ImageName"].ToString(),
                        ImageSize = Convert.ToInt32(row["iamgeSize"]),
                        ImageType = row["imageType"].ToString(),
                        FileExtension = row["FileExtension"].ToString()
                    };
                    images.Add(image);
                }
            }
            return images;
        }

    }
}
