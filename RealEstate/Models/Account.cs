

using System.ComponentModel.DataAnnotations;
namespace Project4.Models
{


    public class Account
    {
        private int? accountID;
        private string accountName;
        private string accountPassword;
        private string accountType;
        private PersonalInfo? personalInfo;
        private WorkInfo? workInfo;
        private bool rememberMe;


        public Account(int? accountID, string accountName, string accountPassword, string accountType, PersonalInfo? personalInfo, WorkInfo? workInfo, bool rememberMe)
        {
            this.accountID = accountID;
            this.accountName = accountName;
            this.accountPassword = accountPassword;
            this.accountType = accountType;
            this.personalInfo = personalInfo;
            this.workInfo = workInfo;
            this.rememberMe = rememberMe;

        }

        public Account()
        {

        }

        public int? AccountID
        {
            get { return accountID; }
            set { accountID = value; }
        }

        [Required(ErrorMessage = "You must enter an account name.")]
        [StringLength(50, ErrorMessage = "Account name cannot exceed 50 characters.")]
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }

        [Required(ErrorMessage = "You must enter an account password.")]
        public string AccountPassword
        {
            get { return accountPassword; }
            set { accountPassword = value; }
        }

        [Required(ErrorMessage = "You must enter an account type.")]
        public string AccountType
        {
            get { return accountType; }
            set { accountType = value; }
        }

        public PersonalInfo? PersonalInfo
        {
            get { return personalInfo; }
            set { personalInfo = value; }
        }

        
        public WorkInfo? WorkInfo
        {
            get { return workInfo; }
            set { workInfo = value; }
        }

        public bool RememberMe
        {
            get { return rememberMe; }
            set { rememberMe = value; }
        }

    }
}
