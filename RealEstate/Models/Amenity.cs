namespace Project4.Models
{
    public class Amenity
    {
        private int amenitiesID;
        private string amenitiesDescription;
        private string amenitiesName;
        
        public Amenity()
        {
        
        }

        public Amenity(int amenitiesID, string amenitiesDescription, string amenitiesName)
        {
            AmenitiesID = amenitiesID;
            AmenitiesDescription = amenitiesDescription;
            AmenitiesName = amenitiesName;
        }

        public int AmenitiesID
        {
            get { return amenitiesID; }
            set { amenitiesID = value; }
        }

        public string AmenitiesDescription
        {
            get { return amenitiesDescription; }
            set { amenitiesDescription = value; }
        }

        public string AmenitiesName
        {
            get { return amenitiesName; }
            set { amenitiesName = value; }
        }
    }
}
