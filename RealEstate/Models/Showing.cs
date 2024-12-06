namespace Project4.Models
{
    public class Showing
    {
        private int? showingID;
        private Contact? contact;
        private string showingDate;
        private Listing? listing;

        public Showing()
        {

        }

        public Showing(int? showingID, Contact? contact, string showingDate, Listing? listing)
        {
            this.showingID = showingID;
            this.contact = contact;
            this.showingDate = showingDate;
            this.listing = listing;
        }

        public int? ShowingID
        {
            get { return showingID; }
            set { showingID = value; }
        }

        public Contact? Contact
        {
            get { return contact; }
            set { contact = value; }
        }

        public string ShowingDate
        {
            get { return showingDate; }
            set { showingDate = value; }
        }


        public Listing Listing
        {
            get { return listing; }
            set { listing = value; }
        }
    }
}