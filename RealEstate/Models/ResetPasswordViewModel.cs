namespace Project4.Models
{
    public class ResetPasswordViewModel
    {
        private Account? account;
        private string newPassword;
        private string confirmPassword;

        public ResetPasswordViewModel()
        {

        }

        public Account Account
        {
            get { return account; }
            set { account = value; }
        }

        public string NewPassword
        {
            get { return newPassword; }
            set { newPassword = value; }
        }

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { confirmPassword = value; }
        }

    }
}
