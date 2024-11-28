using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Project4.Models
{
    public class Home
    {
        private int homeID;
        private Address address;
        private string propertyType;
        private decimal price;
        private int size;
        private int bedrooms;
        private int bathrooms;
        private List<Amenity> amenities;

        public Home(int homeID, Address address, string propertyType, decimal price, int size, int bedrooms, int bathrooms)
        {
            this.homeID = homeID;
            this.address = address;
            this.propertyType = propertyType;
            this.price = price;
            this.size = size;
            this.bedrooms = bedrooms;
            this.bathrooms = bathrooms;
            this.amenities = new List<Amenity>();
        }

        public Home()
        {
            this.amenities = new List<Amenity>();
        }

        public int HomeID
        {
            get { return homeID; }
            set { homeID = value; }
        }

        [Required(ErrorMessage = "You must enter an address.")]
        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        [Required(ErrorMessage = "You must enter a property type.")]
        public string PropertyType
        {
            get { return propertyType; }
            set { propertyType = value; }
        }

        [Required(ErrorMessage = "You must enter a price.")]
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public int Bedrooms
        {
            get { return bedrooms; }
            set { bedrooms = value; }
        }

        public int Bathrooms
        {
            get { return bathrooms; }
            set { bathrooms = value; }
        }

        public List<Amenity> Amenities
        {
            get { return amenities; }
            set { amenities = value; }
        }
    }
}
