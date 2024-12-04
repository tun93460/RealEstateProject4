namespace Project4.Models
{
    public class Showing
    {
        private int showingID;
        private Contact contactInfo;
        private string showingDate;
        private string preferredTime;
        private int showingContactID;
        private int homeID;

        public Showing()
        {

        }

        public Showing(int showingID, Contact contactInfo, string showingDate, string preferredTime, int showingContactID, int homeID)
        {
            this.showingID = showingID;
            this.contactInfo = contactInfo;
            this.showingDate = showingDate;
            this.preferredTime = preferredTime;
            this.showingContactID = showingContactID;
            this.homeID = homeID;
        }

        public int ShowingID
        {
            get { return showingID; }
            set { showingID = value; }
        }

        public Contact ContactInfo
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

        public int ShowingContactID
        {
            get { return showingContactID; }
            set { showingContactID = value; }
        }
        public int HomeID
        {
            get { return homeID; }
            set { homeID = value; }
        }
    }
}