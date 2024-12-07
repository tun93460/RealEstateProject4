namespace Project4.Models
{
    public class AccountRegistrationViewModel
    {
        private Account? account;
        private List<AccountSecurityQuestion>? securityQuestions;
        private List<string> answers;


        public AccountRegistrationViewModel(Account? account, List<AccountSecurityQuestion> securityQuestions, List<string> answers)
        {
            this.account = account;
            this.securityQuestions = securityQuestions;
            this.answers = answers;
        }

        public AccountRegistrationViewModel()
        {

        }

        public Account? Account
        {
            get { return account; }
            set { account = value; }
        }

        public List<AccountSecurityQuestion>? SecurityQuestions
        {
            get { return securityQuestions; }
            set { securityQuestions = value; }
        }

        public List<String> Answers
        {
            get { return answers; }
            set { answers = value; }
        }

    }
}
