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
        public decimal Balance(int customerId)
        {
            return 1000;
        }

        [HttpGet("{customerId}")]
        public List<string> GetAccountsById(int customerId) 
        {
            return new List<string>
            {
                "13258976",
                "16899456"
            };
        }
    }
}
