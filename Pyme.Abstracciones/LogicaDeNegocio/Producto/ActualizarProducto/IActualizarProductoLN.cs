using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyme.Abstracciones.ModelosParaUI;

namespace Pyme.Abstracciones.LogicaDeNegocio.ActualizarProducto
{
    public interface IActualizarProductoLN
    {
        int Actualizar(ProductoDto elProducto);
    }
}
