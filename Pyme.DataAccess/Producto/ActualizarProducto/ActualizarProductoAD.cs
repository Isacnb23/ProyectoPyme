using Pyme.Abstracciones.AccesoADatos.Producto.ActualizarProducto;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.AccesoADatos;
using Pyme.DataAccess.Modelos;
using System.Data.Entity;
using System.Linq;

namespace Pyme.DataAccess.Producto.ActualizarProducto
{
    public class ActualizarProductoAD : IActualizarProductoAD
    {
        private Contexto _contexto;
        public ActualizarProductoAD()
        {
            _contexto = new Contexto();
        }

        public int Actualizar(ProductoDto elProducto)
        {
            ProductoAD elProductoEnBaseDeDatos = _contexto.Producto.Where(Producto => Producto.Id == elProducto.Id).FirstOrDefault();

            // Actualiza campos editables
            elProductoEnBaseDeDatos.Id = elProducto.Id;
            elProductoEnBaseDeDatos.Nombre = elProducto.Nombre;
            elProductoEnBaseDeDatos.CategoriaId = elProducto.CategoriaId;
            elProductoEnBaseDeDatos.Precio = elProducto.Precio;
            elProductoEnBaseDeDatos.ImpuestoPorc = elProducto.ImpuestoPorc;
            elProductoEnBaseDeDatos.Stock = elProducto.Stock;
            EntityState estado = _contexto.Entry(elProductoEnBaseDeDatos).State = System.Data.Entity.EntityState.Modified;
            int cantidadDeDatosAgregados = _contexto.SaveChanges();
            return cantidadDeDatosAgregados;
        }
    }
}
