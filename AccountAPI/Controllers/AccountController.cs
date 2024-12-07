using Microsoft.AspNetCore.Mvc;
using Project4.Models;
using System.Security.Cryptography;
using System.Text;
using MyClassLibrary;


namespace AccountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        Encryption enc = new Encryption();
        AccountDataAccess ada = new AccountDataAccess();

        // POST api/account/authenticate
        [HttpPost("Authenticate")]
        public Boolean Authenticate([FromBody] LoginViewModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.AccountName) || string.IsNullOrEmpty(model.AccountPassword))
                return false;

            Account account = ada.GetAccountByAccountName(model.AccountName);

            if (account != null)
            {
                string decryptedPassword = enc.DecryptPassword(account.AccountPassword);

                if (decryptedPassword == model.AccountPassword)
                {
                    return true;
                }
            }

            return false;
        }

        // POST api/account/register
        [HttpPost("Register")]
        public bool Register([FromBody] AccountRegistrationViewModel registrationModel)
        {
            if (registrationModel == null)
                return false;

            //encrypt password
            string encryptedPassword = enc.EncryptPassword(registrationModel.Account.AccountPassword);

            //set model password as encrypted
            registrationModel.Account.AccountPassword = encryptedPassword;

            //insert record
            int accountID = ada.RegisterAccount(registrationModel.Account);

            return accountID > 0;
        }

        // GET api/account/{id}
        [HttpGet("{id}")]
        public Account GetAccountById(int id)
        {
            Account account = ada.GetAccountByID(id);

            if (account != null)
                return account;

            return account = null;
        }

        // GET api/account/byname/{accountName}
        [HttpGet("ByName/{accountName}")]
        public Account GetAccountByAccountName(string accountName)
        {
            Account account = ada.GetAccountByAccountName(accountName);

            if (account != null)
                return account;

            return account = null;
        }
    }
}
