namespace Project4.Models
{
    public class WorkInfo
    {
        private int workInfoID;
        private Address address;
        private string companyName;
        private string workPhone;
        private string workEmail;

        public WorkInfo(int workInfoID, Address address, string companyName, string workPhone, string workEmail)
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

        public int WorkInfoID
        {
            get { return workInfoID; }
            set { workInfoID = value; }
        }

        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        public String CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        public String WorkPhone
        {
            get { return workPhone; }
            set { workPhone = value; }
        }

        public String WorkEmail
        {
            get { return workEmail; }
            set { workEmail = value; }
        }
    }
}
