using System.ComponentModel.DataAnnotations;

namespace Project4.Models
{
    public class Address
    {
        private int? addressID;
        private string city;
        private string state;
        private string street;
        private string zip;

        public Address(int? addressID, string city, string state, string street, string zip)
        {
            this.addressID = addressID;
            this.city = city;
            this.state = state;
            this.street = street;
            this.zip = zip;
        }

        public Address()
        {

        }

        public int? AddressID
        {
            get { return addressID; }
            set { addressID = value; }
        }

        [Required(ErrorMessage = "City is required")]
        public String City
        {
            get { return city; }
            set { city = value; }
        }

        [Required(ErrorMessage = "State is required")]
        public String State
        {
            get { return state; }
            set { state = value; }
        }

        [Required(ErrorMessage = "Street is required")]
        public String Street
        {
            get { return street; }
            set { street = value; }
        }

        [Required(ErrorMessage = "Zip code is required")]
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
