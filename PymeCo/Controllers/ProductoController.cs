using Microsoft.AspNetCore.Mvc;

namespace PymeCo.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult ListarProducto()
        {
            return View();
        }
    }
}
