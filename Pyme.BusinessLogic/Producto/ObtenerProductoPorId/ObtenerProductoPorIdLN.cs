using Pyme.Abstracciones.AccesoADatos.Producto.ListarProducto;
using Pyme.Abstracciones.AccesoADatos.Producto.ObtenerProductoPorId;
using Pyme.Abstracciones.LogicaDeNegocio.Producto.ObtenerProductoPorId;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.DataAccess.Producto.ListarProducto;
using Pyme.DataAccess.Producto.ObtenerProductoPorId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyme.BusinessLogic.Producto.ObtenerProductoPorId
{
    public class ObtenerProductoPorIdLN : IObtenerProductoPorIdLN
    {
        private IObtenerProductoPorIdAD _obtenerProductoPorId;
        public ObtenerProductoPorIdLN()
        {
            _obtenerProductoPorId = new ObtenerProductoPorIdAD();
        }

        public ProductoDto Obtener(int id)
        {
            ProductoDto elProducto = _obtenerProductoPorId.Obtener(id);
            return elProducto;
        }
    }
}
