using Pyme.Abstracciones.AccesoADatos.Producto.EliminarProducto;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.DataAccess.Modelos;
using Pyme.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.AccesoADatos.Producto.EliminarProducto
{
    public class EliminarProductoAD : IEliminarProductoAD
    {
        private Contexto _contexto;
        public EliminarProductoAD()
        {
            _contexto = new Contexto();
        }

        public int Eliminar(int id)
        {
            ProductoAD elProductoEnBaseDeDatos = _contexto.Producto.Where(Producto => Producto.Id == id).FirstOrDefault();
            _contexto.Producto.Remove(elProductoEnBaseDeDatos);
            EntityState estado = _contexto.Entry(elProductoEnBaseDeDatos).State = System.Data.Entity.EntityState.Deleted;
            int cantidadDeDatosAgregados = _contexto.SaveChanges();
            return cantidadDeDatosAgregados;
        }
    }
}
