﻿using System.Data;
using System.Data.SqlClient;
using Microsoft.JSInterop.Implementation;
using Utilities;

namespace Project4.Models
{
    public class ShowingDataAccess
    {
        HomeDataAccess hda = new HomeDataAccess();
        SqlCommand objCommand = new SqlCommand();
        DBConnect dbObj = new DBConnect();
        DataSet ds = new DataSet();

        public List<Showing> GetShowingsByAccountID(int accountID)
        {
            List<Showing> showings = new List<Showing>();   

            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetShowingByAgentID";

            objCommand.Parameters.AddWithValue("@AccountID", accountID);

            ds = dbObj.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow row in ds.Tables[0].Rows)
                {
                    Showing showing = new Showing
                    {
                        ShowingID = Convert.ToInt32(row["showingID"]),
                        ShowingDate = Convert.ToString(row["showingDate"]),
                        Contact = new Contact
                        {
                            OfferContactID = Convert.ToInt32(row["contactID"]),
                            Name = row["name"].ToString(),
                            Phone = row["phone"].ToString(),
                            Email = row["email"].ToString(),
                            WorkEmail = row["workEmail"].ToString()
                        },
                        Listing = new Listing
                        {
                            ListingID = Convert.ToInt32(row["listingID"]),
                            Home = hda.GetHomeByID(Convert.ToInt32(row["homeID"]))
                        }
                    };
                    showings.Add(showing);
                }
            }
            return showings;
        }

        public int InsertShowing(Showing showing)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertShowing";

            objCommand.Parameters.AddWithValue("@ContactID", showing.Contact.OfferContactID);
            objCommand.Parameters.AddWithValue("@ListingID", showing.Listing.ListingID);
            objCommand.Parameters.AddWithValue("@ShowingTime", showing.ShowingDate);

            SqlParameter outputShowingID = new SqlParameter("@NewShowingID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            objCommand.Parameters.Add(outputShowingID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            int outputID = (outputShowingID.Value != DBNull.Value) ? (int)outputShowingID.Value : 0;

            objCommand.Parameters.Clear();
            return outputID;

        }
    }
}
