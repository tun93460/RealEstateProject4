using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project4.Models;
using Utilities;

namespace MyClassLibrary
{
    public class QuestionDataAccess
    {
        AccountDataAccess ada = new AccountDataAccess();
        DBConnect dbObj = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        DataSet ds = new DataSet();

        public List<AccountSecurityQuestion> GetQuestionsByAccountID(int accountID)
        {
            List<AccountSecurityQuestion> questions = new List<AccountSecurityQuestion>();


            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetQuestionsByAccountID";

            objCommand.Parameters.AddWithValue("@AccountID", accountID);

            ds = dbObj.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    AccountSecurityQuestion question = new AccountSecurityQuestion
                    {
                        AnswerText = row["answerText"].ToString(),
                        Question = new SecurityQuestion
                        {
                            QuestionID = Convert.ToInt32(row["questionID"]),
                            QuestionName = row["questionName"].ToString(),
                            QuestionText = row["questionText"].ToString(),
                        }
                    };
                    questions.Add(question);
                }
            }
            return questions;
        }

        //public int InsertSecurityQuestion()
    }
}
