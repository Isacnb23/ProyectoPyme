using Microsoft.AspNetCore.Mvc;

namespace PymeCo.Controllers.NavigationControllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
