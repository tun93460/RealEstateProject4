namespace Project4.Models
{
    public class Showing
    {
        private int? showingID;
        private Contact? contactInfo;
        private string showingDate;
        private string preferredTime;
        private Home? home;

        public Showing()
        {

        }

        public Showing(int? showingID, Contact? contactInfo, string showingDate, string preferredTime, Home? home)
        {
            this.showingID = showingID;
            this.contactInfo = contactInfo;
            this.showingDate = showingDate;
            this.preferredTime = preferredTime;
            this.home = home;
        }

        public int? ShowingID
        {
            get { return showingID; }
            set { showingID = value; }
        }

        public Contact? ContactInfo
        {
            get { return contactInfo; }
            set { contactInfo = value; }
        }

        public string ShowingDate
        {
            get { return showingDate; }
            set { showingDate = value; }
        }

        public string PreferredTime
        {
            get { return preferredTime; }
            set { preferredTime = value; }
        }

        public Home Home
        {
            get { return home; }
            set { home = value; }
        }
    }
}