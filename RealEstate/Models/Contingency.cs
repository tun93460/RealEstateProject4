using System.ComponentModel.DataAnnotations;

namespace Project4.Models
{
    public class Contingency
    {
        private int? contingencyID;
        private string contingencyName;
        private string contingencyDescription;

        public Contingency()
        {
            this.contingencyDescription = "";
            this.contingencyName = "";
        }

        public Contingency(int? contingencyID, string contingencyDescription, string contingencyName)
        {
            this.contingencyDescription = contingencyDescription;
            this.contingencyName = contingencyName;
        }

        public int? ContingencyID
        {
            get { return this.contingencyID; }
            set { this.contingencyID = value; }

        }


        [Required(ErrorMessage = "Contingency Description is required")]
        public string ContingencyDescription
        {
            get { return contingencyDescription; }
            set { contingencyDescription = value; }
        }

        [Required(ErrorMessage = "Contingency Name is required")]
        public string ContingencyName
        {
            get { return contingencyName; }
            set { contingencyName = value; }
        }
    }
}