using System.Data.SqlClient;
using System.Data;
using Utilities;
using Microsoft.JSInterop.Implementation;

namespace Project4.Models
{
    public class OfferDataAccess
    {
        HomeDataAccess hda = new HomeDataAccess();
        AccountDataAccess ada = new AccountDataAccess();
        DBConnect dbObj = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        DataSet ds = new DataSet();
      
        public List<Offer> GetOffersByAccountID(int accountID)
        {
            List<Offer> offers = new List<Offer>();

            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetOffersByAgentID";

            objCommand.Parameters.AddWithValue("@AccountID", accountID);

            ds = dbObj.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Offer offer = new Offer
                    {
                        OfferID = Convert.ToInt32(row["offerID"]),
                        Amount = Convert.ToDouble(row["offerAmount"]),
                        MoveInDate = Convert.ToDateTime(row["moveInDate"]),
                        OfferStatus = row["offerStatus"].ToString(),
                        SaleType = row["saleType"].ToString(),
                        NeedsToSell = row["needToSellHome"].ToString(),
                        Listing = new Listing
                        {
                            ListingID = Convert.ToInt32(row["listingID"]),
                            Home = hda.GetHomeByID(Convert.ToInt32(row["homeID"])),
                            Account = ada.GetAccountByID(accountID)
                        },
                        Contact = new Contact
                        {
                            OfferContactID = Convert.ToInt32(row["contactID"]),
                            Name = row["name"].ToString(),
                            Phone = row["phone"].ToString(),
                            Email = row["email"].ToString(),
                            WorkEmail = row["workEmail"].ToString()
                        },
                        Contingencies = GetContingenciesByOfferID(Convert.ToInt32(row["offerID"]))
                    };
                    offers.Add(offer);
                }
            }
                return offers;
        }
        public List<Contingency> GetContingenciesByOfferID(int offerID)
        {
            List<Contingency> contingencies = new List<Contingency>();

            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetContingenciesByOfferID";

            objCommand.Parameters.AddWithValue("@OfferID", offerID);

            ds = dbObj.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Contingency contingency = new Contingency
                    {
                        ContingencyName = row["contingenciesName"].ToString(),
                        ContingencyDescription = row["contingenciesDescription"].ToString(),
                    };
                    contingencies.Add(contingency);
                }
            }
            return contingencies;
        }

        public int InsertOffer(Offer offer)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertOffer";

            objCommand.Parameters.AddWithValue("@ListingID", offer.Listing.ListingID);
            objCommand.Parameters.AddWithValue("@ContactID", offer.Contact.OfferContactID);
            objCommand.Parameters.AddWithValue("@OfferAmount", offer.Amount);
            objCommand.Parameters.AddWithValue("@SaleType", offer.SaleType);
            objCommand.Parameters.AddWithValue("@MoveInDate", offer.MoveInDate);
            objCommand.Parameters.AddWithValue("@NeedToSellHome", offer.NeedsToSell);

            SqlParameter outputOfferID = new SqlParameter("@NewOfferID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            objCommand.Parameters.Add(outputOfferID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            int outputID = (outputOfferID.Value != DBNull.Value) ? (int)outputOfferID.Value : 0;

            objCommand.Parameters.Clear();
            return outputID;
        }

        public int InsertListing(Listing listing)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertListing";

            objCommand.Parameters.AddWithValue("@AccountID", listing.Account.AccountID);
            objCommand.Parameters.AddWithValue("@HomeID", listing.Home.HomeID);

            SqlParameter outputListingID = new SqlParameter("@NewListingID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            objCommand.Parameters.Add(outputListingID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            int outputID = (outputListingID.Value != DBNull.Value) ? (int)outputListingID.Value : 0;

            objCommand.Parameters.Clear();
            return outputID;
        }

        public Listing GetListingByHomeID(int homeID)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetListingByHomeID";

            objCommand.Parameters.AddWithValue("@HomeID", homeID);

            DataSet ds = dbObj.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                Listing listing = new Listing
                {
                    ListingID = (int)row["ListingID"],
                    Account = new Account
                    {
                        AccountID = (int)row["AccountID"]
                    },
                    Home = new Home
                    {
                        HomeID = (int)row["HomeID"]
                    }
                };

                return listing;
            }

            return null;
        }


        public int InsertContact(Contact contact)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertContact";

            objCommand.Parameters.AddWithValue("@ContactID", contact.OfferContactID);
            objCommand.Parameters.AddWithValue("@Name", contact.Name);
            objCommand.Parameters.AddWithValue("@Phone", contact.Phone);
            objCommand.Parameters.AddWithValue("@Email", contact.Email);
            objCommand.Parameters.AddWithValue("@WorkEmail", contact.WorkEmail);

            SqlParameter outputContactID = new SqlParameter("@NewContactID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            objCommand.Parameters.Add(outputContactID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            int outputID = (outputContactID.Value != DBNull.Value) ? (int)outputContactID.Value : 0;

            objCommand.Parameters.Clear();
            return outputID;
        }

        public int InsertContingencies(Contingency contingency)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertContingencies";

            objCommand.Parameters.AddWithValue("@ContingenciesType", contingency.ContingencyName);
            objCommand.Parameters.AddWithValue("@ContingenciesDescription", contingency.ContingencyDescription);

            SqlParameter outputContingencyID = new SqlParameter("@NewContingenciesID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            objCommand.Parameters.Add(outputContingencyID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            int outputID = (outputContingencyID.Value != DBNull.Value) ? (int)outputContingencyID.Value : 0;

            objCommand.Parameters.Clear();
            return outputID;

        }

        public void InsertOfferContingencies(int offerID, int contingencyID)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertOfferContingencies";

            objCommand.Parameters.AddWithValue("@OfferID", offerID);
            objCommand.Parameters.AddWithValue("@ContingenciesID", contingencyID);

            dbObj.DoUpdate(objCommand);
        }

        public void DeleteOffer(int offerID)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "DeleteOffer";

            objCommand.Parameters.AddWithValue("@OfferID", offerID);

            dbObj.DoUpdate(objCommand);
        }

    }
}
