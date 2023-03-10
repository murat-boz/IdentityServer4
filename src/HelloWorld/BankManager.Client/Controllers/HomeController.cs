using BankManager.Client.Models;
using BankManager.Client.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
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
            var balance = await this.bankApiService.GetBalanceAsync("BankManager", "banka", "https://localhost:5000/BankA/Balance/1"); //"https://localhost:2000/api/v1/BankA/Balance/1");

            ViewBag.Balance = balance;

            return View();
        }

        public async Task<IActionResult> BankB()
        {
            var balance = await this.bankApiService.GetBalanceAsync("BankManager", "bankb", "https://localhost:5000/BankB/Balance/1"); //"https://localhost:3000/api/v1/BankB/Balance/1");

            ViewBag.Balance = balance;

            return View();
        }

        public async Task Logout()
        {
            await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await base.HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
