using System.ComponentModel.DataAnnotations;
namespace Project4.Models

{

    public class Offer

    {

        private int? offerID;

        private Contact? contactInfo;

        private string amount;

        private string saleType;

        private string needsToSell;

        private string moveInDate;

        private string offerStatus;

        private List<Contingency>? contingencies;

        private Home? home;

        private Account? broker;




        public Offer()

        {

        }



        public Offer(int? offerID, Contact? contactInfo, string amount, string saleType, string needsToSell,

            string moveInDate, string offerStatus, List<Contingency>? contingencies, Home? home, Account? broker)

        {

            this.offerID = offerID;

            this.contactInfo = contactInfo;

            this.amount = amount;

            this.saleType = saleType;

            this.needsToSell = needsToSell;

            this.moveInDate = moveInDate;

            this.offerStatus = offerStatus;

            this.contingencies = contingencies;

            this.home = home;

            this.broker = broker;

        }



        public int? OfferID

        {

            get { return offerID; }

            set { offerID = value; }

        }



        public Contact? ContactInfo

        {

            get { return contactInfo; }

            set { contactInfo = value; }

        }


        [Required(ErrorMessage = "Amount is required")]
        public string Amount

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
        public string MoveInDate

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

        public Home? Home

        {

            get { return home; }

            set { home = value; }

        }



        public Account? Broker

        {

            get { return broker; }

            set { broker = value; }

        }

    }

}