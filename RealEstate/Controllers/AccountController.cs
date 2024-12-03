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
            ViewData["Title"] = "Register";

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
                //register account
                Address persAddress = model.Account.PersonalInfo.Address;
                Address workAddress = model.Account.WorkInfo.Address;
                PersonalInfo personalInfo = model.Account.PersonalInfo;
                WorkInfo workInfo = model.Account.WorkInfo;
                Account account = model.Account;

                //insert personal address
                persAddress.AddressID = ada.RegisterAddress(persAddress.City, persAddress.State, persAddress.Street, persAddress.Zip);

                //insert work address
                workAddress.AddressID = ada.RegisterAddress(workAddress.City, workAddress.State, workAddress.Street, workAddress.Zip);

                //insert personalInfo
                personalInfo.PersonalInfoID = ada.RegisterPersonalInfo(persAddress.AddressID, personalInfo.PersonalPhone, personalInfo.PersonalEmail);

                //insert workInfo
                workInfo.WorkInfoID = ada.RegisterWorkInfo(workAddress.AddressID, workInfo.CompanyName, workInfo.WorkPhone, workInfo.WorkEmail);

                //insert account
                account.AccountID = ada.RegisterAccount(account.AccountName, account.AccountPassword, account.PersonalInfo.PersonalInfoID, account.WorkInfo.WorkInfoID, account.AccountType, account.RememberMe);

                //check for cookies enabled
                if (model.Account.RememberMe)
                {
                    //add cookies
                }

                //count = db result
                if (account.AccountID > 0)
                {
                    //account registered
                    ViewData["Message"] = "Registration successful! Welcome, " + account.AccountName;

                    //add acconut to session
                    HttpContext.Session.SetInt32("Account", account.AccountID);

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
