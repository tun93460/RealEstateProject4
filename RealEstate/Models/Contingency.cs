namespace Project4.Models
{
    public class Contingency
    {
        private string contingencyDescription;

        public Contingency()
        {
            this.contingencyDescription = "";
        }

        public Contingency(string contingencyDescription)
        {
            this.contingencyDescription = contingencyDescription;
        }

        public string ContingencyDescription
        {
            get { return contingencyDescription; }
            set { contingencyDescription = value; }
        }
    }
}