
using Pyme.Abstracciones.ModelosParaUI;

namespace Pyme.Abstracciones.AccesoADatos.Producto.ActualizarProducto
{
    public interface IActualizarProductoAD
    {
        int Actualizar(ProductoDto elProducto);
    }
}
