using Pyme.Abstracciones.AccesoADatos.Producto.ListarProducto;
using Pyme.Abstracciones.LogicaDeNegocio.Producto.ListarProducto;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.DataAccess.Producto.ListarProducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyme.BusinessLogic.Producto.ListarProducto
{
    public class ListarProductoLN : IListarProductoLN
    {
        private IListarProductoAD _listarProductoAD;
        public ListarProductoLN()
        {
            _listarProductoAD = new ListarProductoAD();
        }

        public List<ProductoDto> Obtener()
        {
            /*List<ProductoDto> laListaDeProducto = new List<ProductoDto>();
			laListaDeProducto.Add(ObtenerObjeto());*/
            List<ProductoDto> laListaDeProducto = _listarProductoAD.Obtener();

            return laListaDeProducto;
        }

        
    }
}
