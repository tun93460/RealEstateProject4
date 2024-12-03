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

            SqlParameter outputAccountID = new SqlParameter("@NewAgentID", DBNull.Value)
            {
                Direction = ParameterDirection.Output,
                SqlDbType = SqlDbType.Int,
            };
            objCommand.Parameters.Add(outputAccountID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            outputID = (int)outputAccountID.Value;

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

            SqlParameter outputAddressID = new SqlParameter("@NewAddressID", DBNull.Value)
            {
                Direction = ParameterDirection.Output,
                SqlDbType = SqlDbType.Int,
            };
            objCommand.Parameters.Add(outputAddressID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            outputID = (int)outputAddressID.Value;

            objCommand.Parameters.Clear();
            return outputID;
        }
        


        public int RegisterPersonalInfo(int addressID, string personalPhone, string personalEmail)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertPersonalInfo";

            objCommand.Parameters.AddWithValue("@AddressID", addressID);
            objCommand.Parameters.AddWithValue("@PersonalPhone", personalPhone);
            objCommand.Parameters.AddWithValue("@PersonalEmail", personalEmail);

            SqlParameter outputPersonalInfoID = new SqlParameter("@NewPersonalInfoID", DBNull.Value)
            {
                Direction = ParameterDirection.Output,
                SqlDbType = SqlDbType.Int,
            };
            objCommand.Parameters.Add(outputPersonalInfoID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            outputID = (int)outputPersonalInfoID.Value;

            objCommand.Parameters.Clear();
            return outputID;
        }
        
        public int RegisterWorkInfo(int addressID, string companyName, string workPhone, string workEmail)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertWorkInfo";

            objCommand.Parameters.AddWithValue("@AddressID", addressID);
            objCommand.Parameters.AddWithValue("@CompanyName", companyName);    
            objCommand.Parameters.AddWithValue("@WorkPhone", workPhone);
            objCommand.Parameters.AddWithValue("@WorkEmail", workEmail);

            SqlParameter outputWorkInfoID = new SqlParameter("@NewWorkInfoID", DBNull.Value)
            {
                Direction = ParameterDirection.Output,
                SqlDbType = SqlDbType.Int,
            };
            objCommand.Parameters.Add(outputWorkInfoID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            outputID = (int)outputWorkInfoID.Value;

            objCommand.Parameters.Clear();
            return outputID;
        }
    }
}
