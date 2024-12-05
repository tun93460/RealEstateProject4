namespace Project4.Models
{
    public class Listing
    {
        private int? listingID;
        private Account? account;
        private Home? home;

        public Listing(int? listingID, Account? account, Home? home)
        {
            this.listingID = listingID;
            this.account = account;
            this.home = home;
        }

        public Listing()
        {

        }

        public int? ListingID
        {
            get { return listingID; }
            set { listingID = value; }
        }

        public Account? Account
        {
            get { return account; }
            set { account = value; }
        }

        public Home? Home
        {
            get { return home; }
            set { home = value; }
        }
    }
}
