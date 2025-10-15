using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Pyme.BusinessLogic.Producto.ActualizarProducto;
using Pyme.BusinessLogic.Producto.EliminarProducto;
using Pyme.Abstracciones.LogicaDeNegocio.ActualizarProducto;
using Pyme.Abstracciones.LogicaDeNegocio.CrearProducto;
using Pyme.Abstracciones.LogicaDeNegocio.Producto.EliminarProducto;
using Pyme.Abstracciones.LogicaDeNegocio.Producto.ListarProducto;
using Pyme.Abstracciones.LogicaDeNegocio.Producto.ObtenerProductoPorId;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.BusinessLogic.Producto.CrearProducto;
using Pyme.BusinessLogic.Producto.ListarProducto;
using Pyme.BusinessLogic.Producto.ObtenerProductoPorId;

namespace PymeCo.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IListarProductoLN _listarProducto = new ListarProductoLN();
        private readonly ICrearProductoLN _crearProducto = new CrearProductoLN();
        private readonly IObtenerProductoPorIdLN _obtenerProductoPorId = new ObtenerProductoPorIdLN();
        private readonly IActualizarProductoLN _actualizarProducto = new ActualizarProductoLN();
        private readonly IEliminarProductoLN _eliminarProducto = new EliminarProductoLN();

        // GET: /Producto/ListarProducto
        public ActionResult ListarProducto()
        {
            List<ProductoDto> lista = _listarProducto.Obtener();
            return View(lista);
        }

        // GET: /Producto/DetallesProducto/5
        public ActionResult DetallesProducto(int id)
        {
            var prod = _obtenerProductoPorId.Obtener(id);
            if (prod == null) return HttpNotFound();
            return View(prod);
        }

        // GET: /Producto/CrearProducto
        public ActionResult CrearProducto() => View();

        // POST: /Producto/CrearProducto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CrearProducto(ProductoDto producto /*, HttpPostedFileBase Archivo*/)
        {
            try
            {
                if (!ModelState.IsValid) return View(producto);

                int afectados = await _crearProducto.Guardar(producto);
                return RedirectToAction(nameof(ListarProducto));
            }
            catch
            {
                return View(producto);
            }
        }

        // GET: /Producto/EditarProducto/5
        public ActionResult EditarProducto(int id)
        {
            var prod = _obtenerProductoPorId.Obtener(id);
            if (prod == null) return HttpNotFound();
            return View(prod);
        }

        // POST: /Producto/EditarProducto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProducto(ProductoDto producto /*, HttpPostedFileBase Archivo*/)
        {
            try
            {
                if (!ModelState.IsValid) return View(producto);

                int afectados = _actualizarProducto.Actualizar(producto);
                return RedirectToAction(nameof(ListarProducto));
            }
            catch
            {
                return View(producto);
            }
        }

        // GET: /Producto/EliminarProducto/5
        public ActionResult EliminarProducto(int id)
        {
            var prod = _obtenerProductoPorId.Obtener(id);
            if (prod == null) return HttpNotFound();
            return View(prod);
        }

        // POST: /Producto/EliminarProducto (Confirmación)
        [HttpPost, ActionName("EliminarProducto")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarProductoConfirmado(int id)
        {
            try
            {
                _eliminarProducto.Eliminar(id);
                return RedirectToAction(nameof(ListarProducto));
            }
            catch
            {
                return View();
            }
        }

        // ====== Imagen por Id (opcional, patrón similar a Inventario) ======
        public ActionResult ImagenDeProductoPorId(int id)
        {
            string codigo = id.ToString();
            string carpeta = Server.MapPath("~/Content/Uploads");
            string[] extensiones = { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg" };

            foreach (var ext in extensiones)
            {
                var ruta = Path.Combine(carpeta, codigo + ext);
                if (System.IO.File.Exists(ruta))
                    return File(ruta, ObtenerContentType(ext));
            }
            return PlaceholderImage();
        }

        private ActionResult PlaceholderImage()
        {
            string ruta = Server.MapPath("~/Content/Images/placeholder.svg");
            if (System.IO.File.Exists(ruta))
                return File(ruta, "image/svg+xml");
            return HttpNotFound();
        }

        private string ObtenerContentType(string ext)
        {
            ext = (ext ?? "").ToLowerInvariant();
            if (ext == ".jpg" || ext == ".jpeg") return "image/jpeg";
            if (ext == ".png") return "image/png";
            if (ext == ".gif") return "image/gif";
            if (ext == ".webp") return "image/webp";
            if (ext == ".svg") return "image/svg+xml";
            return "application/octet-stream";
        }

        private void GuardarArchivo(HttpPostedFileBase archivo, string nombreBase)
        {
            if (archivo == null || archivo.ContentLength <= 0 || string.IsNullOrWhiteSpace(nombreBase)) return;

            string carpeta = Server.MapPath("~/Content/Uploads");
            if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

            string extension = Path.GetExtension(archivo.FileName);
            if (string.IsNullOrEmpty(extension)) extension = ".png";
            string destino = Path.Combine(carpeta, nombreBase + extension.ToLowerInvariant());

            foreach (var existente in Directory.GetFiles(carpeta, nombreBase + ".*"))
            {
                try { System.IO.File.Delete(existente); } catch { }
            }

            archivo.SaveAs(destino);
        }
    }
}
