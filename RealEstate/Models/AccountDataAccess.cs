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
        Account account;
        int count;
        int outputID;
        public int AuthenticateAccount(LoginViewModel model)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AuthenticateAccount";

            objCommand.Parameters.AddWithValue("@accountName", model.AccountName);
            objCommand.Parameters.AddWithValue("@accountPassword", model.AccountPassword);

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
            objCommand.Parameters.Clear();
            return -1;
        }

        public int RegisterAccount(string accountName, string accountPassword, int personalInfoID, int workInfoID, string accountType, bool rememberMe)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertAccount";

            objCommand.Parameters.AddWithValue("@DisplayName", accountName);
            objCommand.Parameters.AddWithValue("@UserPassword", accountPassword);
            objCommand.Parameters.AddWithValue("@PersonalInfoID", personalInfoID);
            objCommand.Parameters.AddWithValue("@WorkInfoID", workInfoID);
            objCommand.Parameters.AddWithValue("@AccountType", accountType);
            objCommand.Parameters.AddWithValue("@RememberMe", rememberMe);

            SqlParameter outputAccountID = new SqlParameter("@NewAccountID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            objCommand.Parameters.Add(outputAccountID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            int outputID = (outputAccountID.Value != DBNull.Value) ? (int)outputAccountID.Value : 0;

            objCommand.Parameters.Clear();
            return outputID;
        }

        public int RegisterAddress(string city, string state, string street, string zip)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertAddress";

            objCommand.Parameters.AddWithValue("@City", city);
            objCommand.Parameters.AddWithValue("@State", state);
            objCommand.Parameters.AddWithValue("@Street", street);
            objCommand.Parameters.AddWithValue("@Zip", zip);

            SqlParameter outputAddressID = new SqlParameter("@NewAddressID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            objCommand.Parameters.Add(outputAddressID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            int outputID = (outputAddressID.Value != DBNull.Value) ? (int)outputAddressID.Value : 0;

            objCommand.Parameters.Clear();
            return outputID;
        }

        public int RegisterPersonalInfo(int addressID, string personalPhone, string personalEmail)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertPersonalInfo";

            objCommand.Parameters.AddWithValue("@AddressID", addressID);
            objCommand.Parameters.AddWithValue("@PersonalPhone", personalPhone);
            objCommand.Parameters.AddWithValue("@PersonalEmail", personalEmail);

            SqlParameter outputPersonalInfoID = new SqlParameter("@NewPersonalInfoID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            objCommand.Parameters.Add(outputPersonalInfoID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            int outputID = (outputPersonalInfoID.Value != DBNull.Value) ? (int)outputPersonalInfoID.Value : 0;

            objCommand.Parameters.Clear();
            return outputID;
        }

        public int RegisterWorkInfo(int addressID, string companyName, string workPhone, string workEmail)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InserWorkInfo";

            objCommand.Parameters.AddWithValue("@AddressID", addressID);
            objCommand.Parameters.AddWithValue("@CompanyName", companyName);
            objCommand.Parameters.AddWithValue("@WorkPhone", workPhone);
            objCommand.Parameters.AddWithValue("@WorkEmail", workEmail);

            SqlParameter outputWorkInfoID = new SqlParameter("@NewWorkInfoID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            objCommand.Parameters.Add(outputWorkInfoID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            int outputID = (outputWorkInfoID.Value != DBNull.Value) ? (int)outputWorkInfoID.Value : 0;

            objCommand.Parameters.Clear();
            return outputID;
        }




        public Account GetAccountByAccountName(string accountName)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAccountByAccountName";

            objCommand.Parameters.AddWithValue("@AccountName", accountName);

            // Execute the command to fetch the dataset
            ds = dbObj.GetDataSetUsingCmdObj(objCommand);

            account = null; // Initialize account as null to handle cases where no data is returned

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) // Check if the dataset and rows exist
            {
                DataRow row = ds.Tables[0].Rows[0]; // Assume only one row will be returned for a unique account name

                account = new Account
                {
                    AccountID = Convert.ToInt32(row["accountID"]),
                    AccountName = row["accountName"].ToString(),
                    AccountType = row["accountType"].ToString(),
                    AccountPassword = row["accountPassword"].ToString()
                };
            }

            objCommand.Parameters.Clear(); // Clear parameters to reuse the command object
            return account;
        }

    }
}