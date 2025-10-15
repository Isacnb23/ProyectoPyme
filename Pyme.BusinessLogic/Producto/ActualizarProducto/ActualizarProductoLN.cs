using Inventario.LogicaDeNegocio.General;
using Pyme.Abstracciones.AccesoADatos.Producto.ActualizarProducto;
using Pyme.Abstracciones.LogicaDeNegocio.Producto.ActualizarProducto;
using Pyme.Abstracciones.LogicaDeNegocio.General;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.DataAccess.Producto.ActualizarProducto;

namespace Pyme.BusinessLogic.Producto.ActualizarProducto
{
    public class ActualizarProductoLN : IActualizarProductoLN
    {
        private IActualizarProductoAD _actualizarProductoAD;
        private IFecha _fecha; // opcional

        public ActualizarProductoLN()
        {
            _actualizarProductoAD = new ActualizarProductoAD();
            _fecha = new Fecha(); // opcional
        }

        public int Actualizar(ProductoDto elProducto)
        {
            int cantidadDeResultados = _actualizarProductoAD.Actualizar(elProducto);
            return cantidadDeResultados;
        }
    }
}
