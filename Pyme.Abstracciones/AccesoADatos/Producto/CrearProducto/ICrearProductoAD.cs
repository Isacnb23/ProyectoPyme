using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyme.Abstracciones.ModelosParaUI;

namespace Pyme.Abstracciones.AccesoADatos.Producto.CrearProducto
{
    public interface ICrearProductoAD
    {
        Task<int> Guardar(ProductoDto elProducto);
    }
}
