using Microsoft.AspNetCore.Mvc;

namespace PymeCo.Controllers
{
    public class PedidoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
