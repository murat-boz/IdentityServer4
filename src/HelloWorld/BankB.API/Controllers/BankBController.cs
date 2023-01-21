using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BankB.API.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BankBController : ControllerBase
    {
        [HttpGet("{customerId}")]
        [Authorize(Policy = "ReadBankB")]
        public decimal Balance(int customerId)
        {
            return 500;
        }

        [HttpGet("{customerId}")]
        [Authorize(Policy = "ReadBankB")]
        public List<string> GetAccountsById(int customerId)
        {
            return new List<string>
            {
                "69749464",
                "61314879"
            };
        }

        [HttpGet("{customerId}/{amount}")]
        public bool Invest(int customerId, decimal amount)
        {
            // Do invest

            return true;
        }
    }
}
