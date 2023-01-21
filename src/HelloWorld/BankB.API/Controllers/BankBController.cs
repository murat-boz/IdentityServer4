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
        public decimal Balance(int customerId)
        {
            return 500;
        }

        [HttpGet("{customerId}")]
        public List<string> GetAccountsById(int customerId)
        {
            return new List<string>
            {
                "69749464",
                "61314879"
            };
        }
    }
}
