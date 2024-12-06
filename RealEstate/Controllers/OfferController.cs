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

        public IActionResult Offer()
        {
            Offer offer = new Offer();

            return View(offer);
        }

        public IActionResult SubmitOffer(Offer offer)
        {

            if (ModelState.IsValid)
            {
                //add to database
                //oda.InsertOffer(offer);

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
            //oda.DeleteOffer(offerID)

            List<Offer> offers = oda.GetOffersByAccountID(accountID);

            offers.RemoveAll(o  => o.OfferID == offerID);


            return View("GetOffers");
        }

        public IActionResult AcceptOffer(Offer offer)
        {
            if (offer != null)
            {
                offer.OfferStatus = "Accepted";
                //update offerstatus in db

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

                }
                TempData["Message"] = "Offer with ID: " + offer.OfferID + "has been accepted";
            }
            else
            {
                TempData["Message"] = "Offer with ID: " + offer.OfferID + "was not found";
            }

            return RedirectToAction("GetOffers");
        }



        public IActionResult Email(string offerStatus)
        {
            Email emailObj = new Email();
            String strTo = "tuo84072@temple.edu";
            String? strFrom = HttpContext.Session.GetString("BrokerEmail");
            String strSubject = "Offer Accepted";
            String strMessage = "Congratulations, Your offer has been accepted. Welcome to your new home";
            try
            {
                emailObj.SendMail(strTo, strFrom, strSubject, strMessage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return View();
        }
    }
}
