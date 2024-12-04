namespace Project4.Models
{
    public class Contact
    {
        private int offerContactID;
        private string name;
        private string phone;
        private string email;
        private string workEmail;

        public Contact(int offerContactID, string name, string phone, string email, string workEmail)
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

        public int OfferContactID
        {
            get { return offerContactID; }
            set { offerContactID = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }



        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        public String WorkEmail
        {
            get { return workEmail; }
            set { workEmail = value; }
        }
    }
}