using System.ComponentModel.DataAnnotations;

namespace Project4.Models
{
    public class WorkInfo
    {
        private int? workInfoID;
        private Address? address;
        private string companyName;
        private string workPhone;
        private string workEmail;

        public WorkInfo(int? workInfoID, Address? address, string companyName, string workPhone, string workEmail)
        {
            this.workInfoID = workInfoID;
            this.address = address;
            this.companyName = companyName;
            this.workPhone = workPhone;
            this.workEmail = workEmail;
        }

        public WorkInfo()
        {

        }

        public int? WorkInfoID
        {
            get { return workInfoID; }
            set { workInfoID = value; }
        }

        public Address? Address
        {
            get { return address; }
            set { address = value; }
        }

        [Required(ErrorMessage = "Company name is required")]
        public String CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        [Required(ErrorMessage = "Work phone is required")]
        public String WorkPhone
        {
            get { return workPhone; }
            set { workPhone = value; }
        }

        [Required(ErrorMessage = "Work email is required")]
        public String WorkEmail
        {
            get { return workEmail; }
            set { workEmail = value; }
        }
    }
}
