using Pyme.Abstracciones.AccesoADatos.Producto.CrearProducto;
using Pyme.Abstracciones.LogicaDeNegocio.Producto.CrearProducto;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.DataAccess.Producto.CrearProducto;
using System.Threading.Tasks;

namespace Pyme.BusinessLogic.Producto.CrearProducto
{
    public class CrearProductoLN : ICrearProductoLN
    {
        private readonly ICrearProductoAD _crearProductoAD;

        public CrearProductoLN()
        {
            _crearProductoAD = new CrearProductoAD();
        }

        public Task<int> Guardar(ProductoDto elProducto)
        {
            return _crearProductoAD.Guardar(elProducto);
        }
    }
}
