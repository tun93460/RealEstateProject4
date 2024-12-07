using System.Data.SqlClient;
using System.Data;
using Utilities;
using MyClassLibrary;

namespace Project4.Models
{
    public class AccountDataAccess
    {
        Encryption enc = new Encryption();
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
                int count = Convert.ToInt32(ds.Tables[0].Rows[0]["AccountCount"]);
                if (count > 0) 
                    return count;
                else 
                    return 0;
            }
            return -1;
        }

        public int RegisterAccount(Account account)
        {
            objCommand.Parameters.Clear();

            account.PersonalInfo.Address.AddressID = RegisterAddress(account.PersonalInfo.Address);
            account.WorkInfo.Address.AddressID = RegisterAddress(account.WorkInfo.Address);
            account.PersonalInfo.PersonalInfoID = RegisterPersonalInfo(account.PersonalInfo);
            account.WorkInfo.WorkInfoID = RegisterWorkInfo(account.WorkInfo);

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertAccount";

            objCommand.Parameters.AddWithValue("@DisplayName", account.AccountName);
            objCommand.Parameters.AddWithValue("@UserPassword", account.AccountPassword);
            objCommand.Parameters.AddWithValue("@PersonalInfoID", account.PersonalInfo.PersonalInfoID);
            objCommand.Parameters.AddWithValue("@WorkInfoID", account.WorkInfo.WorkInfoID);
            objCommand.Parameters.AddWithValue("@AccountType", account.AccountType);
            objCommand.Parameters.AddWithValue("@RememberMe", account.RememberMe);

            SqlParameter outputAccountID = new SqlParameter("@NewAccountID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            objCommand.Parameters.Add(outputAccountID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            if (outputAccountID.Value != DBNull.Value)
                return (int)outputAccountID.Value;
            else
                return 0;
        }

        public int RegisterAddress(Address address)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertAddress";

            objCommand.Parameters.AddWithValue("@City", address.City);
            objCommand.Parameters.AddWithValue("@State", address.State);
            objCommand.Parameters.AddWithValue("@Street", address.Street);
            objCommand.Parameters.AddWithValue("@Zip", address.Zip);

            SqlParameter outputAddressID = new SqlParameter
            {
                ParameterName = "@ReturnValue",
                Direction = ParameterDirection.ReturnValue,
                SqlDbType = SqlDbType.Int,

            };
            objCommand.Parameters.Add(outputAddressID);

            dbObj.DoUpdateUsingCmdObj(objCommand);

            int outputID = (outputAddressID.Value != DBNull.Value) ? (int)outputAddressID.Value : 0;

            objCommand.Parameters.Clear();
            return outputID;
        }

        public int RegisterPersonalInfo(PersonalInfo personalInfo)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertPersonalInfo";

            objCommand.Parameters.AddWithValue("@AddressID", personalInfo.Address.AddressID);
            objCommand.Parameters.AddWithValue("@PersonalPhone", personalInfo.PersonalPhone);
            objCommand.Parameters.AddWithValue("@PersonalEmail", personalInfo.PersonalEmail);

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

        public int RegisterWorkInfo(WorkInfo workInfo)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InserWorkInfo";

            objCommand.Parameters.AddWithValue("@AddressID", workInfo.Address.AddressID);
            objCommand.Parameters.AddWithValue("@CompanyName", workInfo.CompanyName);
            objCommand.Parameters.AddWithValue("@WorkPhone", workInfo.WorkPhone);
            objCommand.Parameters.AddWithValue("@WorkEmail", workInfo.WorkEmail);

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

        public Account GetAccountByID(int accountID)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAgentByAgentID";
            objCommand.Parameters.AddWithValue("@AccountID", accountID);

            ds = dbObj.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                return new Account
                {
                    AccountID = Convert.ToInt32(row["accountID"]),
                    AccountName = row["accountName"].ToString(),
                    AccountType = row["accountType"].ToString(),
                    AccountPassword = row["accountPassword"].ToString()
                };
            }

            return null;
        }


        public Account GetAccountByAccountName(string accountName)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAccountByAccountName";
            objCommand.Parameters.AddWithValue("@AccountName", accountName);

            ds = dbObj.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                return new Account
                {
                    AccountID = Convert.ToInt32(row["accountID"]),
                    AccountName = row["accountName"].ToString(),
                    AccountType = row["accountType"].ToString(),
                    AccountPassword = row["accountPassword"].ToString()
                };
            }

            return null;
        }

        public bool UpdateAccountPassword(int accountID, string newPassword)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdatePassword";

            string encryptedPassword = enc.EncryptPassword(newPassword);

            objCommand.Parameters.AddWithValue("@AccountID", accountID);
            objCommand.Parameters.AddWithValue("@NewPassword", encryptedPassword);

            int result = dbObj.DoUpdateUsingCmdObj(objCommand);

            return result > 0;
        }

    }
}