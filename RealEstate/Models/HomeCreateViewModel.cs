using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project4.Models
{
    public class HomeCreateViewModel
    {
        public Home ExistingHome { get; set; }
        public List<Utility> Utilities { get; set; }
        public List<Amenity> Amenities { get; set; }

        public HomeCreateViewModel()
        {
            ExistingHome = new Home();
            Utilities = new List<Utility>();
            Amenities = new List<Amenity>();
        }

        public HomeCreateViewModel(Home home, List<Utility> utilities, List<Amenity> amenities)
        {
            this.ExistingHome = home;
            this.Utilities = utilities;
            this.Amenities = amenities;
        }

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
            private IFormFile imageFile;
            private List<int> selectedUtilities;
            private List<int> selectedAmenities;

            public Home()
            {
                selectedUtilities = new List<int>();
                selectedAmenities = new List<int>();
            }

            public Home(int homeID, Address address, string propertyType, double price, double size,
                int bedrooms, int bathrooms, DateTime dateEntered, string hvacInfo,
                int yearBuilt, string garageType, string homeDesc, string status,
                List<Room> rooms, List<Amenity> amenities, List<Utility> utilities,
                List<HomeImage> homeImages, IFormFile imageFile, List<int> selectedUtilities, List<int> selectedAmenities)
            {
                this.homeID = homeID;
                this.address = address;
                this.propertyType = propertyType;
                this.price = price;
                this.size = size;
                this.bedrooms = bedrooms;
                this.bathrooms = bathrooms;
                this.dateEntered = dateEntered;
                this.hvacInfo = hvacInfo;
                this.yearBuilt = yearBuilt;
                this.garageType = garageType;
                this.homeDesc = homeDesc;
                this.status = status;
                this.rooms = rooms;
                this.amenities = amenities;
                this.utilities = utilities;
                this.homeImages = homeImages;
                this.imageFile = imageFile;
                this.selectedUtilities = selectedUtilities;
                this.selectedAmenities = selectedAmenities;
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

            public DateTime DateEntered
            {
                get { return dateEntered; }
                set { dateEntered = value; }
            }

            public string HvacInfo
            {
                get { return hvacInfo; }
                set { hvacInfo = value; }
            }

            public int YearBuilt
            {
                get { return yearBuilt; }
                set { yearBuilt = value; }
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

            public IFormFile ImageFile
            {
                get { return imageFile; }
                set { imageFile = value; }
            }

            public void AddUtility(Utility utility)
            {
                if (!this.utilities.Contains(utility))
                {
                    this.utilities.Add(utility);
                }
            }

            public void AddAmenity(Amenity amenity)
            {
                if (!this.amenities.Contains(amenity))
                {
                    this.amenities.Add(amenity);
                }
            }

            public List<int> SelectedUtilities
            {
                get { return selectedUtilities; }
                set { selectedUtilities = value; }
            }

            public List<int> SelectedAmenities
            {
                get { return selectedAmenities; }
                set { selectedAmenities = value; }
            }
        }

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

        public class Amenity
        {
            private int amenityID;
            private string amenityType;

            public Amenity()
            {

            }

            public Amenity(int amenityID, string amenityType)
            {
                AmenityID = amenityID;
                AmenityType = amenityType;
            }

            public int AmenityID
            {
                get { return amenityID; }
                set { amenityID = value; }
            }

            public string AmenityType
            {
                get { return amenityType; }
                set { amenityType = value; }
            }
        }

        public class Utility
        {
            private int utilityID;
            private string utilityType;

            public Utility(int utilityID, string utilityType)
            {
                this.utilityID = utilityID;
                this.utilityType = utilityType;
            }

            public Utility()
            {

            }

            public int UtilityID
            {
                get { return utilityID; }
                set { utilityID = value; }
            }

            public String UtilityType
            {
                get { return utilityType; }
                set { utilityType = value; }
            }
        }

        public class Room
        {
            private int roomID;
            private string roomType;
            private string roomDescription;
            private double roomWidth;
            private double roomLength;

            public Room(int roomID, string roomType, string roomDescription, double roomWidth, double roomLength)
            {
                this.roomID = roomID;
                this.roomType = roomType;
                this.roomDescription = roomDescription;
                this.roomWidth = roomWidth;
                this.roomLength = roomLength;
            }

            public Room()
            {

            }

            public int RoomID
            {
                get { return roomID; }
                set { roomID = value; }
            }

            public String RoomType
            {
                get { return roomType; }
                set { roomType = value; }
            }

            public String RoomDescription
            {
                get { return roomDescription; }
                set { roomDescription = value; }
            }

            public double RoomWidth
            {
                get { return roomWidth; }
                set { roomWidth = value; }
            }

            public double RoomLength
            {
                get { return roomLength; }
                set { roomLength = value; }
            }
        }

        public class HomeImage
        {
            private int imageID;
            private byte[] imageData;
            private string imageCaption;

            public HomeImage(int imageID, byte[] imageData, string imageCaption)
            {
                this.imageID = imageID;
                this.imageData = imageData;
                this.imageCaption = imageCaption;
            }

            public HomeImage()
            {
            }

            public int ImageID
            {
                get { return imageID; }
                set { imageID = value; }
            }

            public byte[] ImageData
            {
                get { return imageData; }
                set { imageData = value; }
            }

            public string ImageCaption
            {
                get { return imageCaption; }
                set { imageCaption = value; }
            }
        }
    }
}
