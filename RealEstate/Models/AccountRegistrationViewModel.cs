namespace Project4.Models
{
    public class AccountRegistrationViewModel
    {
        private Account account;
        private List<AccountSecurityQuestion> securityQuestions;

        public AccountRegistrationViewModel(Account account, List<AccountSecurityQuestion> securityQuestions)
        {
            this.account = account;
            this.securityQuestions = securityQuestions;
        }

        public AccountRegistrationViewModel()
        {

        }

        public Account Account
        {
            get { return account; }
            set { account = value; }
        }

        public List<AccountSecurityQuestion> SecurityQuestions
        {
            get { return securityQuestions; }
            set { securityQuestions = value; }
        }

    }
}
