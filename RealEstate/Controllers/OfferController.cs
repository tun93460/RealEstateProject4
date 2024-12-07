using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project4.Models;
using Project4.Models.Utilities;


namespace Project4.Controllers
{
    public class OfferController : Controller
    {
        OfferDataAccess oda = new OfferDataAccess();

        public IActionResult Offer(int id)
        {
            Offer offer = new Offer
            {
                Contact = new Contact(),
                Listing = oda.GetListingByHomeID(id),
            };


            return View(offer);
        }

        public IActionResult SaveOffer(Offer offer)
        {

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                // Log or print each validation error
                ViewData["Error"] = error.ErrorMessage;  // Or log it to a file, etc.
            }

            if (ModelState.IsValid)
            {
                //add offer to database
                offer.OfferID = oda.InsertOffer(offer);
                offer.Contact.OfferContactID = oda.InsertContact(offer.Contact);
                for (int i = 0; i < offer.Contingencies.Count; i++)
                {
                    offer.Contingencies[i].ContingencyID = oda.InsertContingencies(offer.Contingencies[i]);
                    oda.InsertOfferContingencies((int)offer.OfferID, (int)offer.Contingencies[i].ContingencyID);
                }

                //redirect to home view
                ViewData["Message"] = "Your offer has been submitted.";
                return RedirectToAction("Home", "Index");

            }

            return RedirectToAction("Offer", offer);
        }

        public IActionResult AddContingency(Offer offer, string contingencyName, string contingencyDescription)
        {
            if (offer.Contingencies == null)
            {
                offer.Contingencies = new List<Contingency>();
            }

            offer.Contingencies.Add(new Contingency
            {
                ContingencyName = contingencyName,
                ContingencyDescription = contingencyDescription
            });

            return View("Offer", offer);
        }

        public IActionResult GetOffers(int accountID)
        {
            List<Offer> offers = oda.GetOffersByAccountID(accountID);

            return View(offers);
        }

        public IActionResult DenyOffer(int offerID, int accountID)
        {
            //delete offer
            oda.DeleteOffer(offerID);

            List<Offer> offers = oda.GetOffersByAccountID(accountID);

            offers.RemoveAll(o  => o.OfferID == offerID);


            return View("GetOffers");
        }

        public IActionResult AcceptOffer(Offer offer)
        {
            if (offer != null)
            {


                Email emailObj = new Email();
                try
                {
                    string strTo = offer.Contact.Email;
                    string strFrom = offer.Listing.Account.WorkInfo.WorkEmail;
                    string strSubject = "Offer Accepted";
                    string strMessage = "Congratulations! Your offer has been accepted.";

                    emailObj.SendMail(strTo, strFrom, strSubject, strMessage);
                }
                catch (Exception ex) 
                {
                    Debug.WriteLine("Error sending email: " + ex.Message);
                    TempData["Error"] = "Unable to send email.";
                }
                TempData["Message"] = "Offer with ID: " + offer.OfferID + "has been accepted";
            }
            else
            {
                TempData["Message"] = "Offer with ID: " + offer.OfferID + "was not found";
            }

            return RedirectToAction("GetOffers");
        }

    }
}
