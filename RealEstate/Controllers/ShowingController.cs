using System;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project4.Models;
using Project4.Models.Utilities;

namespace Project4.Controllers
{
    public class ShowingController : Controller
    {
        ShowingDataAccess sda = new ShowingDataAccess();

        public IActionResult Showing()
        {
            Showing showing = new Showing();

            return View(showing);
        }

        public IActionResult SubmitShowing(Showing showing)
        {
            if (ModelState.IsValid)
            {
                //add to db
                //sda.InsertShowing(showing);

                ViewData["Message"] = "Your showing has been submitted.";

                return RedirectToAction("Home", "Index");
            }

            return View("Showing", showing);
        }

        public IActionResult GetShowings(int accountID)
        {
            List<Showing> showings = sda.GetShowingsByAccountID(accountID);

            return View(showings);
        }


    }
}
