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

    }
}
