
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace PymeCo.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult ListarCliente()
        {
            return View();
        }
    }
}
