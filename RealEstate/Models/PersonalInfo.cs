using System.Net;

namespace Project4.Models
{
    public class PersonalInfo
    {
        private int personalInfoID;
        private Address address;
        private string personalPhone;
        private string personalEmail;
        private string name;

        public PersonalInfo(int personalInfoID, Address address, string personalPhone, string personalEmail, string name)
        {
            this.personalInfoID = personalInfoID;
            this.address = address;
            this.personalPhone = personalPhone;
            this.personalEmail = personalEmail;
            this.name = name;
        }

        public PersonalInfo()
        {

        }

        public int PersonalInfoID
        {
            get { return personalInfoID; }
            set { personalInfoID = value; }
        }

        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        public String PersonalPhone
        {
            get { return personalPhone; }
            set { personalPhone = value; }
        }

        public String PersonalEmail
        {
            get { return personalEmail; }
            set { personalEmail = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}

