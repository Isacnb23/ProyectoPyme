using Pyme.Abstracciones.AccesoADatos.Producto.EliminarProducto;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.DataAccess.Modelos;
using Pyme.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyme.DataAccess.Producto.EliminarProducto
{
    public class EliminarProductoAD : IEliminarProductoAD
    {
        private readonly Contexto _contexto;

        public EliminarProductoAD()
        {
            _contexto = new Contexto();
        }

        public int Eliminar(int id)
        {
            var entidad = _contexto.Producto.FirstOrDefault(p => p.Id == id);
            if (entidad == null) return 0;

            _contexto.Producto.Remove(entidad);
            return _contexto.SaveChanges();
        }
    }
}
