namespace Project4.Models
{
    public class Amenity
    {
        public string AmenityDescription { get; set; }
        public int HomeID { get; set; }

        public Amenity()
        {
            AmenityDescription = "";
            HomeID = 0;
        }

        public Amenity(string amenityDescription, int homeID)
        {
            AmenityDescription = amenityDescription;
            HomeID = homeID;
        }
    }
}
