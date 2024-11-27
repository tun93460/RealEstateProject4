using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace Project4.Models
{
    public class AccountDataAccess
    {
        DBConnect dbObj = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        DataSet ds = new DataSet();
        int count;
        public int AuthenticateAccount(Account account)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AuthenticateAccount";

            objCommand.Parameters.AddWithValue("@accountName", account.AccountName);
            objCommand.Parameters.AddWithValue("@accountPassword", account.AccountPassword);

            ds = dbObj.GetDataSet(objCommand);

            if (ds.Tables[0].Rows.Count > 0)
            {
                count = Convert.ToInt32(ds.Tables[0].Rows[0]["AccountCount"]);

                if (count > 0)
                {
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            return -1;
        }
    }
}
