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

        public DataSet GetAllHomes(
            string location = null,
            string propertyType = null,
            int minBedrooms = 0,
            int minBathrooms = 0,
            int minPrice = 0,
            int maxPrice = 0)
        {
            SqlCommand cmdSearchHomes = new SqlCommand("SearchHomes");
            cmdSearchHomes.CommandType = CommandType.StoredProcedure;

            if (!string.IsNullOrEmpty(location))
                cmdSearchHomes.Parameters.AddWithValue("@Location", location);
            else
                cmdSearchHomes.Parameters.AddWithValue("@Location", DBNull.Value);

            if (!string.IsNullOrEmpty(propertyType))
                cmdSearchHomes.Parameters.AddWithValue("@PropertyType", propertyType);
            else
                cmdSearchHomes.Parameters.AddWithValue("@PropertyType", DBNull.Value);

            if (minBedrooms > 0)
                cmdSearchHomes.Parameters.AddWithValue("@MinBedrooms", minBedrooms);
            else
                cmdSearchHomes.Parameters.AddWithValue("@MinBedrooms", DBNull.Value);

            if (minBathrooms > 0)
                cmdSearchHomes.Parameters.AddWithValue("@MinBathrooms", minBathrooms);
            else
                cmdSearchHomes.Parameters.AddWithValue("@MinBathrooms", DBNull.Value);

            if (minPrice > 0)
                cmdSearchHomes.Parameters.AddWithValue("@MinPrice", minPrice);
            else
                cmdSearchHomes.Parameters.AddWithValue("@MinPrice", DBNull.Value);

            if (maxPrice > 0)
                cmdSearchHomes.Parameters.AddWithValue("@MaxPrice", maxPrice);
            else
                cmdSearchHomes.Parameters.AddWithValue("@MaxPrice", DBNull.Value);

            return dbConnect.GetDataSetUsingCmdObj(cmdSearchHomes);
        }

        public DataSet SearchHomes(string location, string propertyType, int minBedrooms, int minBathrooms, decimal? minPrice, decimal? maxPrice)
        {
            SqlCommand cmdSearchHomes = new SqlCommand("SearchHomes");
            cmdSearchHomes.CommandType = CommandType.StoredProcedure;

            cmdSearchHomes.Parameters.AddWithValue("@Location", location);

            if (!string.IsNullOrEmpty(propertyType))
                cmdSearchHomes.Parameters.AddWithValue("@PropertyType", propertyType);
            else
                cmdSearchHomes.Parameters.AddWithValue("@PropertyType", DBNull.Value);

            if (minBedrooms > 0)
                cmdSearchHomes.Parameters.AddWithValue("@MinBedrooms", minBedrooms);
            else
                cmdSearchHomes.Parameters.AddWithValue("@MinBedrooms", DBNull.Value);

            if (minBathrooms > 0)
                cmdSearchHomes.Parameters.AddWithValue("@MinBathrooms", minBathrooms);
            else
                cmdSearchHomes.Parameters.AddWithValue("@MinBathrooms", DBNull.Value);

            if (minPrice.HasValue)
                cmdSearchHomes.Parameters.AddWithValue("@MinPrice", minPrice.Value);
            else
                cmdSearchHomes.Parameters.AddWithValue("@MinPrice", DBNull.Value);

            if (maxPrice.HasValue)
                cmdSearchHomes.Parameters.AddWithValue("@MaxPrice", maxPrice.Value);
            else
                cmdSearchHomes.Parameters.AddWithValue("@MaxPrice", DBNull.Value);

            return dbConnect.GetDataSetUsingCmdObj(cmdSearchHomes);
        }

        public DataSet GetAmenitiesByHomeID(int homeID)
        {
            SqlCommand cmdGetAmenities = new SqlCommand("GetAmenitiesByHomeID");
            cmdGetAmenities.CommandType = CommandType.StoredProcedure;
            cmdGetAmenities.Parameters.AddWithValue("@HomeID", homeID);

            return dbConnect.GetDataSetUsingCmdObj(cmdGetAmenities);
        }
    }
}
