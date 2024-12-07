using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Project4.Models
{
    public class HomeCreateViewModel
    {
        private int? homeID;
        private int? addressID;
        private string city;
        private string state;
        private string street;
        private string zip;
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
        private int roomID;
        private string roomType;
        private string roomDescription;
        private double roomWidth;
        private double roomLength;
        private List<Room> rooms;
        private List<Amenity> amenities;
        private List<Utility> utilities;
        private List<HomeImage> homeImages;
        private IFormFile imageFile;
        private List<int> selectedUtilities;
        private List<int> selectedAmenities;

        public HomeCreateViewModel()
        {
            Rooms = new List<Room>();
            Utilities = new List<Utility>();
            Amenities = new List<Amenity>();
            HomeImages = new List<HomeImage>();
            SelectedUtilities = new List<int>();
            SelectedAmenities = new List<int>();
        }

        public int? HomeID
        {
            get { return homeID; }
            set { homeID = value; }
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
}
