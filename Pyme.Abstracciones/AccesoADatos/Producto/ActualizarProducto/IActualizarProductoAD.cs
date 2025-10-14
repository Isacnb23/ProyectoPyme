using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyme.Abstracciones.ModelosParaUI;

namespace Pyme.Abstracciones.AccesoADatos.Producto.ActualizarProducto
{
    public interface IActualizarProductoAD
    {
        int Actualizar(ProductoDto elProducto);
    }
}
