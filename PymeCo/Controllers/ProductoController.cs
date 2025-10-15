using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pyme.BusinessLogic.Producto.ActualizarProducto;
using Pyme.BusinessLogic.Producto.EliminarProducto;
using Pyme.Abstracciones.LogicaDeNegocio.ActualizarProducto;
using Pyme.Abstracciones.LogicaDeNegocio.CrearProducto;
//using Pyme.Abstracciones.LogicaDeNegocio.Producto.ActualizarProducto;
//using Pyme.Abstracciones.LogicaDeNegocio.Producto.CrearProducto;
using Pyme.Abstracciones.LogicaDeNegocio.Producto.EliminarProducto;
using Pyme.Abstracciones.LogicaDeNegocio.Producto.ListarProducto;
using Pyme.Abstracciones.LogicaDeNegocio.Producto.ObtenerProductoPorId;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.BusinessLogic.Producto.CrearProducto;
using Pyme.BusinessLogic.Producto.ListarProducto;
using Pyme.BusinessLogic.Producto.ObtenerProductoPorId;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PymeCo.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IListarProductoLN _listarProducto = new ListarProductoLN();
        private readonly ICrearProductoLN _crearProducto = new CrearProductoLN();
        private readonly IObtenerProductoPorIdLN _obtenerProductoPorId = new ObtenerProductoPorIdLN();
        private readonly IActualizarProductoLN _actualizarProducto = new ActualizarProductoLN();
        private readonly IEliminarProductoLN _eliminarProducto = new EliminarProductoLN();

        private readonly IWebHostEnvironment _env;
        public ProductoController(IWebHostEnvironment env) => _env = env;

        // GET: /Producto/ListarProducto
        public IActionResult ListarProducto()
        {
            List<ProductoDto> lista = _listarProducto.Obtener();
            return View(lista);
        }

        // GET: /Producto/DetallesProducto/5
        public IActionResult DetallesProducto(int id)
        {
            var prod = _obtenerProductoPorId.Obtener(id);
            if (prod == null) return NotFound();
            return View(prod);
        }

        // GET: /Producto/CrearProducto
        public IActionResult CrearProducto() => View();

        // POST: /Producto/CrearProducto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearProducto(ProductoDto producto)
        {
            try
            {
                if (!ModelState.IsValid) return View(producto);

                // === Manejo de imagen (opcional, si tu DTO tiene IFormFile Archivo) ===
                // if (producto.Archivo != null && producto.Archivo.Length > 0)
                // {
                //     GuardarArchivo(producto.Archivo, producto.Id.ToString());
                // }
                // producto.EstadoProducto = true; // default Activo si querés

                int afectados = await _crearProducto.Guardar(producto);
                return RedirectToAction(nameof(ListarProducto));
            }
            catch
            {
                return View(producto);
            }
        }

        // GET: /Producto/EditarProducto/5
        public IActionResult EditarProducto(int id)
        {
            var prod = _obtenerProductoPorId.Obtener(id);
            if (prod == null) return NotFound();
            return View(prod);
        }

        // POST: /Producto/EditarProducto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarProducto(ProductoDto producto)
        {
            try
            {
                if (!ModelState.IsValid) return View(producto);

                // === Manejo de imagen (opcional)
                // if (producto.Archivo != null && producto.Archivo.Length > 0)
                // {
                //     GuardarArchivo(producto.Archivo, producto.Id.ToString());
                // }

                int afectados = _actualizarProducto.Actualizar(producto);
                return RedirectToAction(nameof(ListarProducto));
            }
            catch
            {
                return View(producto);
            }
        }

        // GET: /Producto/EliminarProducto/5
        public IActionResult EliminarProducto(int id)
        {
            var prod = _obtenerProductoPorId.Obtener(id);
            if (prod == null) return NotFound();
            return View(prod);
        }

        // POST: /Producto/EliminarProducto (Confirmación)
        [HttpPost, ActionName("EliminarProducto")]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarProductoConfirmado(int id)
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

        // ====== Imagen por Id (opcional, igual al patrón de Inventario) ======
        public IActionResult ImagenDeProductoPorId(int id)
        {
            string codigo = id.ToString();
            string carpeta = Path.Combine(_env.WebRootPath, "Content", "Uploads");
            string[] extensiones = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg" };

            foreach (var ext in extensiones)
            {
                var ruta = Path.Combine(carpeta, codigo + ext);
                if (System.IO.File.Exists(ruta))
                    return PhysicalFile(ruta, ObtenerContentType(ext));
            }
            return PlaceholderImage();
        }

        private IActionResult PlaceholderImage()
        {
            string ruta = Path.Combine(_env.WebRootPath, "Content", "Images", "placeholder.svg");
            if (System.IO.File.Exists(ruta))
                return PhysicalFile(ruta, "image/svg+xml");
            return NotFound();
        }

        private string ObtenerContentType(string ext) => ext.ToLowerInvariant() switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".svg" => "image/svg+xml",
            _ => "application/octet-stream"
        };

        private void GuardarArchivo(IFormFile archivo, string nombreBase)
        {
            if (archivo == null || archivo.Length <= 0 || string.IsNullOrWhiteSpace(nombreBase)) return;

            string carpeta = Path.Combine(_env.WebRootPath, "Content", "Uploads");
            if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

            string extension = Path.GetExtension(archivo.FileName);
            if (string.IsNullOrEmpty(extension)) extension = ".png";
            string destino = Path.Combine(carpeta, nombreBase + extension.ToLowerInvariant());

            foreach (var existente in Directory.GetFiles(carpeta, nombreBase + ".*"))
            {
                try { System.IO.File.Delete(existente); } catch { }
            }

            using var fs = new FileStream(destino, FileMode.Create);
            archivo.CopyTo(fs);
        }
    }
}
