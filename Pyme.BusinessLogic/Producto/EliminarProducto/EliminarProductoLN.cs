using Pyme.Abstracciones.AccesoADatos.Producto.EliminarProducto;
using Pyme.Abstracciones.LogicaDeNegocio.Producto.EliminarProducto;
using Pyme.DataAccess.Producto.EliminarProducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyme.BusinessLogic.Producto.EliminarProducto
{
    public class EliminarProductoLN : IEliminarProductoLN
    {
        private readonly IEliminarProductoAD _eliminarProductoAD;

        public EliminarProductoLN()
        {
            _eliminarProductoAD = new EliminarProductoAD();
        }

        public int Eliminar(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido", nameof(id));
            return _eliminarProductoAD.Eliminar(id);
        }
    }
}

