using Microsoft.AspNetCore.Mvc;

namespace PymeCo.Controllers.NavigationControllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
