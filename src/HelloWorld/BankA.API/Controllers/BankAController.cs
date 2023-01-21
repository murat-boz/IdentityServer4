using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BankA.API.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BankAController : ControllerBase
    {
        [HttpGet("{customerId}")]
        [Authorize(Policy = "ReadBankA")]
        public decimal Balance(int customerId)
        {
            return 1000;
        }

        [HttpGet("{customerId}")]
        [Authorize(Policy = "ReadBankA")]
        public List<string> GetAccountsById(int customerId) 
        {
            return new List<string>
            {
                "13258976",
                "16899456"
            };
        }

        [HttpGet("{customerId}/{amount}")]
        [Authorize(Policy = "WriteBankA")]
        public bool Invest(int customerId, decimal amount)
        {
            // Do invest

            return true;
        }
    }
}
