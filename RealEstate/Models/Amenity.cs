namespace Project4.Models
{
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
}
