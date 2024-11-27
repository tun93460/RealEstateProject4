namespace Project4.Models
{
    public class AccountSecurityQuestion
    {
        private string answerText;
        private Account account;
        private SecurityQuestion question;

        public AccountSecurityQuestion(string answerText, Account account, SecurityQuestion question)
        {
            this.answerText = answerText;
            this.account = account;
            this.question = question;
        }

        public string AnswerText
        {
            get { return answerText; }
            set { answerText = value; }
        }

        public Account Account
        {
            get { return account; }
            set { account = value; }
        }

        public SecurityQuestion Question
        {
            get { return question; }
            set { question = value; }
        }
    }
}
