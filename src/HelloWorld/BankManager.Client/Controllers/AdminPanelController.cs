using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankManager.Client.Controllers
{
    [Authorize]
    public class AdminPanelController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AdminPanelController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var claims = User.Claims;

            var items = (await this.httpContextAccessor.HttpContext.AuthenticateAsync()).Properties.Items;

            return View();
        }
    }
}
