using System.ComponentModel.DataAnnotations;

namespace Project4.Models
{
    public class Contact
    {
        private int? offerContactID;
        private string name;
        private string phone;
        private string email;
        private string? workEmail;

        public Contact(int? offerContactID, string name, string phone, string email, string? workEmail)
        {
            this.offerContactID = offerContactID;
            this.name = name;
            this.phone = phone;
            this.email = email;
            this.workEmail = workEmail;
        }

        public Contact()
        {

        }

        public int? OfferContactID
        {
            get { return offerContactID; }
            set { offerContactID = value; }
        }

        [Required(ErrorMessage = "Name is required")]
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        [Required(ErrorMessage = "Phone number is required")]
        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }


        [Required(ErrorMessage = "Email is required")]
        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        public String? WorkEmail
        {
            get { return workEmail; }
            set { workEmail = value; }
        }
    }
}