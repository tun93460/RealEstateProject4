using Microsoft.AspNetCore.Mvc;
using Project4.Models;

namespace RealEstate.Controllers
{
    public class AccountController : Controller
    {
        AccountDataAccess ada = new AccountDataAccess();
        Account account;
        int count;

        public IActionResult Logout()
        {
            // clear the session
            HttpContext.Session.Clear();

            // clear login cookies
            Response.Cookies.Delete("Name");
            Response.Cookies.Delete("Password");

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";

            //check for login cookies
            if (Request.Cookies.ContainsKey("Name") && Request.Cookies.ContainsKey("Password"))
            {
                
                //set account role in session
                HttpContext.Session.SetString("AccountRole", Request.Cookies["Name"]);

                return RedirectToAction("Index", "Home");
            }
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            ViewData["Title"] = "Login";

            if (ModelState.IsValid)
            {
                // Call stored procedure to authenticate
                count = ada.AuthenticateAccount(model);

                if (count > 0)
                {
                    // Account found, get role for session
                    //account = ada.GetAccountByAccountName(model.AccountName);

                    HttpContext.Session.SetString("AccountName", model.AccountName);
                    
                    //check for cookies
                    if (model.RememberMe)
                    {
                        CookieOptions options = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(7),
                            HttpOnly = true,
                            Secure = true,
                            IsEssential = true
                        };
                        Response.Cookies.Append("Name", model.AccountName, options);
                        Response.Cookies.Append("Password", model.AccountPassword, options);
                    }
                    ViewData["Message"] = "Welcome! " + model.AccountName;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Message"] = "Invalid username or password.";
                }
            }
            else
            {
                ViewData["Message"] = "Please fill in all required fields.";
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View(new AccountRegistrationViewModel());
        }

        public IActionResult Register(AccountRegistrationViewModel model)
        {
            ViewData["Title"] = "Register";

            //List<AccountSecurityQuestion> questionList = new List<AccountSecurityQuestion>();
            ////get security questions from db, populate questionList ^
            ////populate security question ddl
            ////here are some test questions
            //SecurityQuestion ques1 = new SecurityQuestion(1, "Pet Name", "Please enter the name of your first pet: ");
            //SecurityQuestion ques2 = new SecurityQuestion(2, "Street Name", "What is the name of the street you grew up on? ");

            //questionList.Add(new AccountSecurityQuestion("Bella", model.Account, ques1));
            //questionList.Add(new AccountSecurityQuestion("Kingswood", model.Account, ques2));



            //ViewData["SecurityQuestions"] = questionList;
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                ViewData["Message"] = string.Join("<br>", errors);
            }

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
                personalInfo.PersonalInfoID = ada.RegisterPersonalInfo((int)persAddress.AddressID, personalInfo.PersonalPhone, personalInfo.PersonalEmail);

                //insert workInfo
                workInfo.WorkInfoID = ada.RegisterWorkInfo((int)workAddress.AddressID, workInfo.CompanyName, workInfo.WorkPhone, workInfo.WorkEmail);

                //insert account
                account.AccountID = ada.RegisterAccount(account.AccountName, account.AccountPassword, (int)account.PersonalInfo.PersonalInfoID, (int)account.WorkInfo.WorkInfoID, account.AccountType, account.RememberMe);

                //check if acconut was entered successfully
                if (account.AccountID > 0)
                {
                    //check for login cookies
                    if (account.RememberMe)
                    {
                        //create cookie
                        CookieOptions options = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(7),
                            HttpOnly = true,
                            Secure = true,
                            IsEssential = true,
                        };
                        Response.Cookies.Append("Name", account.AccountName, options);
                        Response.Cookies.Append("Password", account.AccountPassword, options);
                    }
                    
                    //account registered
                    ViewData["Message"] = "Registration successful! Welcome, " + account.AccountName;

                    //add account role to session
                    HttpContext.Session.SetString("AccountRole", account.AccountType);

                    //redirect registered user to homepage
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //account not added
                    //ViewData["Message"] = "Something went wrong when entering your credentials";
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    ViewData["Message"] = string.Join("<br>", errors);
                }
            }
            return View();
        }

        public IActionResult ForgotPassword()
        {
            
            return View();
        }
    }
}
