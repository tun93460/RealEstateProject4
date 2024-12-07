using Microsoft.AspNetCore.Mvc;
using Project4.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Data;
using MyClassLibrary;

namespace RealEstate.Controllers
{
    public class AccountController : Controller
    {
        Encryption enc = new Encryption();
        QuestionDataAccess qda = new QuestionDataAccess();
        AccountDataAccess ada = new AccountDataAccess();
        int count;

        private readonly string webApiUrl = "http://localhost:7033/api/Account/";

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
                
                //set account in session
                Account account = GetAccount(Request.Cookies["Name"]);
                HttpContext.Session.SetString("Account", JsonConvert.SerializeObject(account));

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
                //encrypt password
                model.AccountPassword = enc.EncryptPassword(model.AccountPassword);

                //replace with api call
                //count = ada.AuthenticateAccount(model);
                string modelJson = JsonConvert.SerializeObject(model);

                //create http webrequest
                WebRequest request = WebRequest.Create(webApiUrl + "Authenticate/");
                request.Method = "POST";
                request.ContentLength = modelJson.Length;
                request.ContentType = "application/json";

                //write json to web request
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(modelJson);
                writer.Flush();
                writer.Close();

                //read data from the web response
                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                string data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                //deserialize data
                bool result = JsonConvert.DeserializeObject<bool>(data);

                if (result)
                {
                    //replace with api call
                    //account = ada.GetAccountByAccountName(model.AccountName);

                    //place account in session
                    Account account = GetAccount(model.AccountName);

                    HttpContext.Session.SetString("Account", JsonConvert.SerializeObject(account));
                    
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

            if (ModelState.IsValid)
            {
                Account account = model.Account;

                List<AccountSecurityQuestion> questions = model.SecurityQuestions;
                for (int i = 0; i < 3; i++)
                {
                    questions.Add(new AccountSecurityQuestion
                    {
                        AnswerText = model.Answers[i],
                        Question = model.SecurityQuestions[i].Question,
                    });
                }

                //encrypt password
                account.AccountPassword = enc.EncryptPassword(account.AccountPassword);
                
                //replace with api call
                //account.AccountID = ada.RegisterAccount(account);
                string modelJson = JsonConvert.SerializeObject(model);

                //create http webrequest
                WebRequest request = WebRequest.Create(webApiUrl + "Register/");
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = modelJson.Length;

                //wrtie json to web request
                StreamWriter writer = new StreamWriter(request.GetRequestStream()); 
                writer.Write(modelJson);
                writer.Flush();
                writer.Close();

                //read data from the web response
                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                string data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                //deserialize data
                int result = JsonConvert.DeserializeObject<int>(data);


                //check if account was entered successfully
                if (result > 0)
                {
                    account.AccountID = result;
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
                    HttpContext.Session.SetString("Account", JsonConvert.SerializeObject(account));

                    //redirect registered user to homepage
                    return RedirectToAction("Index", "Home");
                }    
            }
            else
            {
                //account not added
                //ViewData["Message"] = "Something went wrong when entering your credentials";
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                ViewData["Message"] = string.Join("<br>", errors);
            }
            return View(model);
        }

        public IActionResult ForgotPassword(int accountID)
        {
            List<AccountSecurityQuestion> questions = qda.GetQuestionsByAccountID(accountID);

            Random random = new Random();

            int randomIndex = random.Next(questions.Count);

            AccountSecurityQuestion question = questions[randomIndex];
                
            if (question != null)
            {
                //display question and get answer
                return View("SecurityQuestion", question);
            }
            
            return View("Error");
        }

        
        public IActionResult VerifySecurityQuestion(int accountID, int questionID, string answerText)
        {
            List<AccountSecurityQuestion> questions = qda.GetQuestionsByAccountID(accountID);

            AccountSecurityQuestion question = questions
                .FirstOrDefault(q => q.Question.QuestionID == questionID);
            
            if (question != null)
            {
                if (question.AnswerText == answerText)
                {
                    return RedirectToAction("ResetPassword");
                }
                else
                {
                    TempData["ErrorMessage"] = "Incorrect answer. Please try again.";
                    return View("ForgotPassword", question);
                }
            }
            else
            {
                return View("Error");
            }

        }

        public IActionResult ResetPassword()
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel
            {
                Account = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString("Account")),
                NewPassword = "",
                ConfirmPassword = "",
            };

            return View(model);
        }

        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (model.NewPassword != model.ConfirmPassword)
            {
                TempData["ErrorMessage"] = "Passwords do not match";
                return View(model);
            }

            bool result = true; // = ada.UpdatePassword(model.AccountID, model.NewPassword);

            if (result)
            {
                TempData["Message"] = "Your Password has been reset successfully";
                return RedirectToAction("Login");
            }

            TempData["ErrorMessage"] = "An error occured while resetting your password. Pleaes try again";
            return View(model);
        }



        private Account GetAccount(string accountName)
        {
            WebRequest request = WebRequest.Create(webApiUrl + "ByName/" + accountName);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            Account account = JsonConvert.DeserializeObject<Account>(data);

            return account;
        }
    }
}
