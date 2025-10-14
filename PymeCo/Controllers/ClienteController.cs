using Microsoft.AspNetCore.Mvc;

namespace PymeCo.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult ListarCliente()
        {
            return View();
        }
    }
}
