using BankManager.Client.Models;
using BankManager.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankManager.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IBankApiService bankApiService;

        public HomeController(ILogger<HomeController> logger, IBankApiService bankApiService)
        {
            this.logger         = logger;
            this.bankApiService = bankApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> BankA()
        {
            var balance = await this.bankApiService.GetBalanceAsync("BankA", "banka", "https://localhost:2000/api/v1/BankA/Balance/1");

            ViewBag.Balance = balance;

            return View();
        }

        public async Task<IActionResult> BankB()
        {
            var balance = await this.bankApiService.GetBalanceAsync("BankB", "bankb", "https://localhost:3000/api/v1/BankB/Balance/1");

            ViewBag.Balance = balance;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
