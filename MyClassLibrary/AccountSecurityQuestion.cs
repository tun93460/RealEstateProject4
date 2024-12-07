namespace Project4.Models
{
    public class AccountSecurityQuestion
    {
        private string answerText;
        private string accountID;
        private SecurityQuestion? question;

        public AccountSecurityQuestion(string answerText, string accountID, SecurityQuestion? question)
        {
            this.answerText = answerText;
            this.accountID = accountID;
            this.question = question;
        }

        public AccountSecurityQuestion()
        {

        }

        public string AnswerText
        {
            get { return answerText; }
            set { answerText = value; }
        }

        public string AccountID
        {
            get { return accountID; }
            set { accountID = value; }
        }

        public SecurityQuestion? Question
        {
            get { return question; }
            set { question = value; }
        }
    }
}
