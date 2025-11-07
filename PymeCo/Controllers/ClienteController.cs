using Pyme.Abstracciones.LogicaDeNegocio.Cliente.ActualizarCliente;
using Pyme.Abstracciones.LogicaDeNegocio.Cliente.CrearCliente;
using Pyme.Abstracciones.LogicaDeNegocio.Cliente.EliminarCliente;
using Pyme.Abstracciones.LogicaDeNegocio.Cliente.ListarCliente;
using Pyme.Abstracciones.LogicaDeNegocio.Cliente.ObtenerClientePorId;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.BusinessLogic.Cliente.ActualizarCliente;
using Pyme.BusinessLogic.Cliente.CrearCliente;
using Pyme.BusinessLogic.Cliente.EliminarCliente;
using Pyme.BusinessLogic.Cliente.ListarCliente;
using Pyme.BusinessLogic.Cliente.ObtenerClientePorId;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeCo.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IListarClienteLN _listarCliente = new ListarClienteLN();
        private readonly ICrearClienteLN _crearCliente = new CrearClienteLN();
        private readonly IObtenerClientePorIdLN _obtenerClientePorId = new ObtenerClientePorIdLN();
        private readonly IActualizarClienteLN _actualizarCliente = new ActualizarClienteLN();
        private readonly IEliminarClienteLN _eliminarCliente = new EliminarClienteLN();

        // GET: /Cliente/ListarCliente
        public ActionResult ListarCliente()
        {
            List<ClienteDto> lista = _listarCliente.Obtener();
            return View(lista);
        }

        // GET: /Cliente/DetallesCliente/5
        public ActionResult DetallesCliente(int id)
        {
            var cliente = _obtenerClientePorId.Obtener(id);
            if (cliente == null) return HttpNotFound();
            return View(cliente);
        }

        // GET: /Cliente/CrearCliente
        public ActionResult CrearCliente() => View();

        // POST: /Cliente/CrearCliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CrearCliente(ClienteDto cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            try
            {
                int afectados = await _crearCliente.Guardar(cliente);

                if (afectados <= 0)
                {
                    ModelState.AddModelError("", "No se realizó ninguna inserción.");
                    return View(cliente);
                }

                TempData["Ok"] = "Cliente creado correctamente.";
                return RedirectToAction(nameof(ListarCliente));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar: " + ex.Message);
                System.Diagnostics.Debug.WriteLine(ex);
                return View(cliente);
            }
        }

        // GET: /Cliente/EditarCliente/5
        [HttpGet]
        public ActionResult EditarCliente(int id)
        {
            var cliente = _obtenerClientePorId.Obtener(id);
            if (cliente == null) return HttpNotFound();
            return View(cliente);
        }

        // POST: /Cliente/EditarCliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCliente(ClienteDto cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            try
            {
                int afectados = _actualizarCliente.Actualizar(cliente);

                if (afectados <= 0)
                {
                    ModelState.AddModelError("", "No se actualizó ningún registro.");
                    return View(cliente);
                }

                TempData["Ok"] = "Cliente actualizado correctamente.";
                return RedirectToAction(nameof(ListarCliente));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar: " + ex.Message);
                return View(cliente);
            }
        }

        // GET: /Cliente/EliminarCliente/5
        public ActionResult EliminarCliente(int id)
        {
            if (id <= 0) return HttpNotFound();
            var cliente = _obtenerClientePorId.Obtener(id);
            if (cliente == null) return HttpNotFound();
            return View(cliente);
        }

        //// POST: /Cliente/EliminarCliente (Confirmación)
        //[HttpPost, ActionName("EliminarCliente")]
        //[ValidateAntiForgeryToken]
        //public ActionResult EliminarClienteConfirmado(int id)
        //{
        //    try
        //    {
        //        int afectados = _eliminarCliente.Eliminar(id);
        //        if (afectados > 0)
        //        {
        //            TempData["Ok"] = "Cliente eliminado correctamente.";
        //        }
        //        else
        //        {
        //            TempData["Error"] = "El cliente no existe o ya fue eliminado.";
        //        }
        //        return RedirectToAction(nameof(ListarCliente));
        //    }
        //    catch (System.Data.Entity.Infrastructure.DbUpdateException)
        //    {
        //        TempData["Error"] = "No se puede eliminar: el cliente está relacionado con otros registros.";
        //        return RedirectToAction(nameof(EliminarCliente), new { id });
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error"] = "Ocurrió un error al eliminar: " + ex.Message;
        //        return RedirectToAction(nameof(EliminarCliente), new { id });
        //    }
        //}
        [HttpPost, ActionName("EliminarCliente")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarClienteConfirmado(int id /* o ClienteDto model */)
        {
            try
            {
                // var id = model.Id;  // si usas ClienteDto
                int afectados = _eliminarCliente.Eliminar(id);

                TempData[afectados > 0 ? "Ok" : "Error"] =
                    afectados > 0 ? "Cliente eliminado correctamente."
                                  : "El cliente no existe o ya fue eliminado.";

                return RedirectToAction(nameof(ListarCliente));
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.InnerException?.Message);
                TempData["Error"] = "No se puede eliminar: el cliente está relacionado con otros registros.";
                return RedirectToAction(nameof(EliminarCliente), new { id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ocurrió un error al eliminar: " + ex.Message;
                return RedirectToAction(nameof(EliminarCliente), new { id });
            }
        }


    }
}
