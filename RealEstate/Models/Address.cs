namespace Project4.Models
{
    public class Address
    {
        private int addressID;
        private string propType;
        private string city;
        private string state;
        private string street;
        private string zip;

        public Address(int addressID, string propType, string city, string state, string street, string zip)
        {
            this.addressID = addressID;
            this.propType = propType;
            this.city = city;
            this.state = state;
            this.street = street;
            this.zip = zip;
        }

        public Address()
        {

        }

        public int AddressID
        {
            get { return addressID; }
            set { addressID = value; }
        }

        public String PropType
        {
            get { return propType; }
            set { propType = value; }
        }

        public String City
        {
            get { return city; }
            set { city = value; }
        }

        public String State
        {
            get { return state; }
            set { state = value; }
        }

        public String Street
        {
            get { return street; }
            set { street = value; }
        }

        public String Zip
        {
            get { return zip; }
            set { zip = value; }
        }

        public override string ToString()
        {
            return street + " " + city + ", " + state + " " + zip;
        }
    }
}
