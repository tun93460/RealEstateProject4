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
                foreach (DataRow row in ds.Tables[0].Rows)
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

		public List<Amenity> GetAmenities()
		{
			SqlCommand cmdGetAmenities = new SqlCommand("GetAllAmenities");
			cmdGetAmenities.CommandType = CommandType.StoredProcedure;
			cmdGetAmenities.Parameters.Clear(); 

			ds = dbConnect.GetDataSetUsingCmdObj(cmdGetAmenities);

			List<Amenity> amenities = new List<Amenity>(); 

			if (ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					Amenity amenity = new Amenity
					{
						AmenityID = Convert.ToInt32(row["amenitiesID"]),
						AmenityType = row["AmenitiesType"].ToString()
					};
					amenities.Add(amenity);
				}
			}

			cmdGetAmenities.Parameters.Clear();
			return amenities;
		}

		public List<Utility> GetUtilities()
		{
			objCommand.Parameters.Clear();
			objCommand.CommandType = CommandType.StoredProcedure;
			objCommand.CommandText = "GetAllUtilities";
			objCommand.Parameters.Clear(); 

			ds = dbConnect.GetDataSetUsingCmdObj(objCommand);

			List<Utility> utilities = new List<Utility>(); 

			if (ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					Utility utility = new Utility
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
                        AmenityID = Convert.ToInt32(row["amenitiesID"]),
                        AmenityType = row["AmenitiesType"].ToString()
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
                        ImageData = row["imageData"] as Byte[]
                    };
                    images.Add(image);
                }
            }
            return images;
        }

        public int CreateHome(string propertyType, int homeSizeTotal, int bedroomCount, int bathroomCount, string yearBuilt, string garage, string description, string askingPrice, string status, string listingDate, int homeAddressID, int brokerID)
        {
            SqlCommand cmdHome = new SqlCommand("InsertHome");
            cmdHome.CommandType = CommandType.StoredProcedure;
            cmdHome.Parameters.Clear();

            cmdHome.Parameters.AddWithValue("@PropType", propertyType);
            cmdHome.Parameters.AddWithValue("@HomeSize", homeSizeTotal);
            cmdHome.Parameters.AddWithValue("@NumBedrooms", bedroomCount);
            cmdHome.Parameters.AddWithValue("@NumBathrooms", bathroomCount);
            cmdHome.Parameters.AddWithValue("@YearBuilt", yearBuilt);
            cmdHome.Parameters.AddWithValue("@GarageType", garage);
            cmdHome.Parameters.AddWithValue("@HomeDesc", description);
            cmdHome.Parameters.AddWithValue("@AskingPrice", askingPrice);
            cmdHome.Parameters.AddWithValue("@Status", status);
            cmdHome.Parameters.AddWithValue("@DateEntered", DateTime.Now);

            cmdHome.Parameters.AddWithValue("@HomeAddressID", homeAddressID);
            cmdHome.Parameters.AddWithValue("@BrokerID", brokerID);

            object result = dbConnect.ExecuteScalarFunction(cmdHome);
            return Convert.ToInt32(result);
        }

        public int CreateAmenities(string amenitiesType)
        {
            SqlCommand cmdAmenities = new SqlCommand("InsertAmenities");
            cmdAmenities.CommandType = CommandType.StoredProcedure;
            cmdAmenities.Parameters.Clear();

            cmdAmenities.Parameters.AddWithValue("@AmenitiesType", amenitiesType);

            object result = dbConnect.ExecuteScalarFunction(cmdAmenities);
            return Convert.ToInt32(result);
        }

        public int CreateRoom(string roomType, string roomDescription, decimal roomWidth, decimal roomLength)
        {
            SqlCommand cmdRoom = new SqlCommand("InsertRoom");
            cmdRoom.CommandType = CommandType.StoredProcedure;
            cmdRoom.Parameters.Clear();

            cmdRoom.Parameters.AddWithValue("@RoomType", roomType);
            cmdRoom.Parameters.AddWithValue("@RoomDescription", roomDescription);
            cmdRoom.Parameters.AddWithValue("@RoomWidth", roomWidth);
            cmdRoom.Parameters.AddWithValue("@RoomLength", roomLength);

            object result = dbConnect.ExecuteScalarFunction(cmdRoom);
            return Convert.ToInt32(result);
        }

        public int CreateUtilities(string utilitiesType)
        {
            SqlCommand cmdUtilities = new SqlCommand("InsertUtilities");
            cmdUtilities.CommandType = CommandType.StoredProcedure;
            cmdUtilities.Parameters.Clear();

            cmdUtilities.Parameters.AddWithValue("@UtilitiesType", utilitiesType);

            object result = dbConnect.ExecuteScalarFunction(cmdUtilities);
            return Convert.ToInt32(result);
        }

        public int CreateImage(byte[] imageData, string imageCaption)
        {
            SqlCommand cmdImage = new SqlCommand("InsertImage");
            cmdImage.CommandType = CommandType.StoredProcedure;
            cmdImage.Parameters.Clear();

            cmdImage.Parameters.AddWithValue("@ImageData", imageData);
            cmdImage.Parameters.AddWithValue("@ImageCaption", imageCaption);

            object result = dbConnect.ExecuteScalarFunction(cmdImage);
            return Convert.ToInt32(result);
        }

        public int CreateAddress(string city, string state, string street, int zip)
        {
            SqlCommand cmdAddress = new SqlCommand("InsertAddress");
            cmdAddress.CommandType = CommandType.StoredProcedure;
            cmdAddress.Parameters.Clear();

            cmdAddress.Parameters.AddWithValue("@City", city);
            cmdAddress.Parameters.AddWithValue("@State", state);
            cmdAddress.Parameters.AddWithValue("@Street", street);
            cmdAddress.Parameters.AddWithValue("@Zip", zip);

            object result = dbConnect.ExecuteScalarFunction(cmdAddress);
            return Convert.ToInt32(result);
        }

        public void LinkHomeWithAddress(int homeID, int addressID)
        {
            SqlCommand cmdHomeAddress = new SqlCommand("InsertHomeAddress");
            cmdHomeAddress.CommandType = CommandType.StoredProcedure;
            cmdHomeAddress.Parameters.Clear();

            cmdHomeAddress.Parameters.AddWithValue("@HomeID", homeID);
            cmdHomeAddress.Parameters.AddWithValue("@AddressID", addressID);

            dbConnect.ExecuteScalarFunction(cmdHomeAddress);
        }

        public void LinkHomeWithAmenities(int homeID, int amenitiesID)
        {
            SqlCommand cmdHomeAmenities = new SqlCommand("InsertHomeAmenities");
            cmdHomeAmenities.CommandType = CommandType.StoredProcedure;
            cmdHomeAmenities.Parameters.Clear();

            cmdHomeAmenities.Parameters.AddWithValue("@HomeID", homeID);
            cmdHomeAmenities.Parameters.AddWithValue("@AmenitiesID", amenitiesID);

            dbConnect.ExecuteScalarFunction(cmdHomeAmenities);
        }

        public void LinkHomeWithRoom(int homeID, int roomID)
        {
            SqlCommand cmdHomeRoom = new SqlCommand("InsertHomeRoom");
            cmdHomeRoom.CommandType = CommandType.StoredProcedure;
            cmdHomeRoom.Parameters.Clear();

            cmdHomeRoom.Parameters.AddWithValue("@HomeID", homeID);
            cmdHomeRoom.Parameters.AddWithValue("@RoomID", roomID);

            dbConnect.ExecuteScalarFunction(cmdHomeRoom);
        }

        public void LinkHomeWithUtilities(int homeID, int utilitiesID)
        {
            SqlCommand cmdHomeUtilities = new SqlCommand("InsertHomeUtilities");
            cmdHomeUtilities.CommandType = CommandType.StoredProcedure;
            cmdHomeUtilities.Parameters.Clear();

            cmdHomeUtilities.Parameters.AddWithValue("@HomeID", homeID);
            cmdHomeUtilities.Parameters.AddWithValue("@UtilitiesID", utilitiesID);

            dbConnect.ExecuteScalarFunction(cmdHomeUtilities);
        }

        public void LinkHomeWithImage(int homeID, int imageID)
        {
            SqlCommand cmdHomeImage = new SqlCommand("InsertHomeImage");
            cmdHomeImage.CommandType = CommandType.StoredProcedure;
            cmdHomeImage.Parameters.Clear();

            cmdHomeImage.Parameters.AddWithValue("@HomeID", homeID);
            cmdHomeImage.Parameters.AddWithValue("@ImageID", imageID);

            dbConnect.ExecuteScalarFunction(cmdHomeImage);
        }

        public void LinkHomeWithBroker(int homeID, int brokerID)
        {
            SqlCommand cmdHomeBroker = new SqlCommand("InsertHomeBroker");
            cmdHomeBroker.CommandType = CommandType.StoredProcedure;
            cmdHomeBroker.Parameters.Clear();

            cmdHomeBroker.Parameters.AddWithValue("@HomeID", homeID);
            cmdHomeBroker.Parameters.AddWithValue("@BrokerID", brokerID);

            dbConnect.ExecuteScalarFunction(cmdHomeBroker);
        }

    }
}
