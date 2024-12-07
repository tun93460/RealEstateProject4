using System.ComponentModel.DataAnnotations;

namespace Project4.Models
{
    public class LoginViewModel
    {
        private string accountName;
        private string accountPassword;
        private bool rememberMe;

        public LoginViewModel() { }


        [Required(ErrorMessage = "You must enter a username.")]
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }

        [Required(ErrorMessage = "You must enter a password.")]
        public string AccountPassword
        {
            get { return accountPassword; }
            set { accountPassword = value; }
        }

        public bool RememberMe
        {
            get { return rememberMe; }
            set { rememberMe = value; }
        }
    }
}

