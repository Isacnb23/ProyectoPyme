using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyme.Abstracciones.ModelosParaUI;

namespace Pyme.Abstracciones.AccesoADatos.Producto.ObtenerProductoPorId
{
    public interface IObtenerProductoPorIdAD
    {
        ProductoDto Obtener(int id);
    }
}
