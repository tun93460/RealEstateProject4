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

        public IActionResult Offer()
        {
            Offer offer = new Offer
            {
                Contingencies = new List<Contingency>()
            };

            TempData["Offer"] = JsonConvert.SerializeObject(offer);
            return View(offer);
        }

        public IActionResult SubmitOffer()
        {

            string offerJson = TempData["Offer"].ToString();
            Offer offer;

            if (string.IsNullOrEmpty(offerJson))
            {
                offer = new Offer { Contingencies = new List<Contingency>() };
            }
            else
            {
                offer = JsonConvert.DeserializeObject<Offer>(offerJson);
            }


            if (ModelState.IsValid)
            {
                //add to database

                //clear current offer from temp data
                TempData.Remove("Offer");

                //redirect to home view
                ViewData["Message"] = "Your offer has been submitted.";
                return RedirectToAction("Home", "Index");

            }

            return RedirectToAction("Offer");
        }

        public IActionResult AddContingency(string contingencyName, string contingencyDescription)
        {
            //get offer from tempdata
            string offerJson = TempData["Offer"].ToString();
            Offer offer;

            if (string.IsNullOrEmpty(offerJson))
            {
                offer = new Offer { Contingencies = new List<Contingency>() };
            }
            else
            {
                offer = JsonConvert.DeserializeObject<Offer>(offerJson);
            }

            if (offer == null)
            {
                offer = new Offer
                {
                    Contingencies = new List<Contingency>()
                };
            }

            if (offer.Contingencies == null)
            {
                offer.Contingencies = new List<Contingency>();
            }

            offer.Contingencies.Add(new Contingency
            {
                ContingencyName = contingencyName,
                ContingencyDescription = contingencyDescription
            });

            TempData["Offer"] = JsonConvert.SerializeObject(offer);

            return View("Offer", offer);




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
        }
    }
}
