namespace Project4.Models

{

    public class Offer

    {

        private int offerID;

        private Contact contactInfo;

        private string amount;

        private string saleType;

        private string needsToSell;

        private string moveInDate;

        private string offerStatus;

        private List<Contingency> contingencies;

        private int homeID;

        private int brokerID;

        private int offerContactID;



        public Offer()

        {

        }



        public Offer(int offerID, Contact contactInfo, string amount, string saleType, string needsToSell,

            string moveInDate, string offerStatus, List<Contingency> contingencies, int homeID, int brokerID, int offerContactID)

        {

            this.offerID = offerID;

            this.contactInfo = contactInfo;

            this.amount = amount;

            this.saleType = saleType;

            this.needsToSell = needsToSell;

            this.moveInDate = moveInDate;

            this.offerStatus = offerStatus;

            this.contingencies = contingencies;

            this.homeID = homeID;

            this.brokerID = brokerID;

            this.offerContactID = offerContactID;

        }



        public int OfferID

        {

            get { return offerID; }

            set { offerID = value; }

        }



        public Contact ContactInfo

        {

            get { return contactInfo; }

            set { contactInfo = value; }

        }



        public string Amount

        {

            get { return amount; }

            set { amount = value; }

        }



        public string SaleType

        {

            get { return saleType; }

            set { saleType = value; }

        }



        public string NeedsToSell

        {

            get { return needsToSell; }

            set { needsToSell = value; }

        }



        public string MoveInDate

        {

            get { return moveInDate; }

            set { moveInDate = value; }

        }



        public string OfferStatus

        {

            get { return offerStatus; }

            set { offerStatus = value; }

        }



        public List<Contingency> Contingencies

        {

            get { return contingencies; }

            set { contingencies = value; }

        }

        public int HomeID

        {

            get { return homeID; }

            set { homeID = value; }

        }



        public int BrokerID

        {

            get { return brokerID; }

            set { brokerID = value; }

        }



        public int OfferContactID

        {

            get { return offerContactID; }

            set { offerContactID = value; }

        }

    }

}