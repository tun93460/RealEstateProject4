using System.ComponentModel.DataAnnotations;
namespace Project4.Models

{

    public class Offer

    {

        private int? offerID;

        private Contact? contact;

        private double amount;

        private string saleType;

        private string needsToSell;

        private DateTime moveInDate;

        private string offerStatus;

        private List<Contingency>? contingencies;

        private Listing? listing;




        public Offer()

        {

        }



        public Offer(int? offerID, Contact? contact, double amount, string saleType, string needsToSell,

            DateTime moveInDate, string offerStatus, List<Contingency>? contingencies, Listing? listing)

        {

            this.offerID = offerID;

            this.contact = contact;

            this.amount = amount;

            this.saleType = saleType;

            this.needsToSell = needsToSell;

            this.moveInDate = moveInDate;

            this.offerStatus = offerStatus;

            this.contingencies = contingencies;

            this.listing = listing;

        }



        public int? OfferID

        {

            get { return offerID; }

            set { offerID = value; }

        }



        public Contact? Contact

        {

            get { return contact; }

            set { contact = value; }

        }


        [Required(ErrorMessage = "Amount is required")]
        public double Amount

        {

            get { return amount; }

            set { amount = value; }

        }


        [Required(ErrorMessage = "Sale type is required")]
        public string SaleType

        {

            get { return saleType; }

            set { saleType = value; }

        }


        [Required(ErrorMessage = "Field is required")]
        public string NeedsToSell

        {

            get { return needsToSell; }

            set { needsToSell = value; }

        }


        [Required(ErrorMessage = "Move in date required")]
        public DateTime MoveInDate

        {

            get { return moveInDate; }

            set { moveInDate = value; }

        }


        public string? OfferStatus

        {

            get { return offerStatus; }

            set { offerStatus = value; }

        }



        public List<Contingency>? Contingencies

        {

            get { return contingencies; }

            set { contingencies = value; }

        }

        public Listing? Listing
        {
            get { return listing; }
            set { listing = value; }
        }

    }

}