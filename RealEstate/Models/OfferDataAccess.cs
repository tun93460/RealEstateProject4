using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace Project4.Models
{
    public class OfferDataAccess
    {
        HomeDataAccess hda = new HomeDataAccess();
        AccountDataAccess ada = new AccountDataAccess();
        DBConnect dbObj = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        DataSet ds = new DataSet();

        public List<Offer> GetOffersByAgentID(int accountID)
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
                        Listing = new Listing
                        {
                            ListingID = Convert.ToInt32(row["listingID"]),
                            Home = hda.GetHomeByID(Convert.ToInt32(row["homeID"]))
                            //Account = ada.GetAccountByID(accountID)
                        },
                        ContactInfo = new Contact
                        {
                            OfferContactID = Convert.ToInt32(row["contactID"]),
                            Name = row["name"].ToString(),
                            Phone = row["phone"].ToString(),
                            Email = row["email"].ToString(),
                            WorkEmail = row["workEmail"].ToString()
                        },



                    };
                }
            }
        }
        public List<Contingency> GetContingenciesByOfferID(int offerID)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetContingenciesByOfferID";

            objCommand.Parameters.AddWithValue("@OfferID", offerID);

            ds = dbObj.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables[0].)
        }
    }
}
