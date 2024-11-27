using Microsoft.AspNetCore.Mvc;
using Project4.Models;

namespace RealEstate.Controllers
{
    public class AccountController : Controller
    {
        AccountDataAccess ada = new AccountDataAccess();
        int count;

        [HttpGet]
        public IActionResult Login()
        {
            return View(new Account());
        }

        [HttpPost]
        public IActionResult Login(Account account)
        {
            ViewData["Title"] = "Login";

            if (!ModelState.IsValid)
            {
                // Log or inspect the errors in the ModelState
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                //call stored procedure to check if account exists
                //returns 1 if account found
                //maybe replace with API call
                count = ada.AuthenticateAccount(account);

                //check remember me to set cookies
                if (account.RememberMe)
                {
                    //set cookie for account creds
                }

                if (count > 0)
                {
                    //account found
                    ViewData["Message"] = "Login successful! Welcome, " + account.AccountName;

                    //add acconut to session
                    HttpContext.Session.SetInt32("Account", account.AccountID);

                    //redirect authenticated user to homepage
                    return RedirectToAction("Index", "Home");
                }
                else if (count == 0)
                {
                    //no acconuts found
                    ViewData["Message"] = "Invalid username or password.";
                }
            }
            else
            {
                //invalid model
                ViewData["Message"] = "Please fill in all required fields.";
            }

            return View(account);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new AccountRegistrationViewModel());
        }

        public IActionResult Register(AccountRegistrationViewModel model)
        {
            List<AccountSecurityQuestion> questionList = new List<AccountSecurityQuestion>();
            //get security questions from db, populate questionList ^
            //populate security question ddl
            //here are some test questions
            SecurityQuestion ques1 = new SecurityQuestion(1, "Pet Name", "Please enter the name of your first pet: ");
            SecurityQuestion ques2= new SecurityQuestion(2, "Street Name", "What is the name of the street you grew up on? ");

            questionList.Add(new AccountSecurityQuestion("Bella", model.Account, ques1));
            questionList.Add(new AccountSecurityQuestion("Kingswood", model.Account, ques2));
                                
            

            ViewData["SecurityQuestions"] = questionList;

            if (ModelState.IsValid)
            {
                //register account - ada.RegisterAccount(account)
                //don't forget to insert security quetions

                //check for cookies enabled
                if (model.Account.RememberMe)
                {
                    //add cookies
                }

                //count = db result
                if (count > 0)
                {
                    //account registered
                    ViewData["Message"] = "Registration successful! Welcome, " + model.Account.AccountName;

                    //add acconut to session
                    HttpContext.Session.SetInt32("Account", model.Account.AccountID);

                    //redirect registered user to homepage
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //account not added
                    ViewData["Message"] = "Something went wrong when entering your credentials";
                }

            }

            return View();
        }
    }
}
