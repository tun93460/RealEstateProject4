using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Project4.Models
{
    public class Home
    {
        private int homeID;
        private Address address;
        private string propertyType;
        private double price;
        private double size;
        private int bedrooms;
        private int bathrooms;
        private DateTime dateEntered;
        private string hvacInfo;
        private int yearBuilt;
        private string garageType;
        private string homeDesc;
        private string status;
        private List<Room> rooms;
        private List<Amenity> amenities;
        private List<Utility> utilities;
        private List<HomeImage> homeImages;



        public Home()
        {
        }

        public DateTime DateEntered
        {
            get { return dateEntered; }
            set { dateEntered = value; }
        }

        public string HvacInfo
        {
            get { return hvacInfo; }
            set {  hvacInfo = value; }
        }

        public int YearBuilt
        {
            get { return yearBuilt; }
            set {  yearBuilt = value; }
        }

        public string GarageType
        {
            get { return garageType; }
            set { garageType = value; }
        }

        public string HomeDesc
        {
            get { return homeDesc; }
            set { homeDesc = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
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
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public double Size
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

        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        public List<Utility> Utilities
        {
            get { return utilities; }
            set { utilities = value; }
        }

        public List<Amenity> Amenities
        {
            get { return amenities; }
            set { amenities = value; }
        }

        public List<HomeImage> HomeImages
        {
            get { return homeImages; }
            set { homeImages = value; }
        }
    }
}
