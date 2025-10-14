using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyme.Abstracciones.ModelosParaUI
{
    
    public static class EstadosDeProducto
    {
        public static bool Activo = true;
        public static bool Inactivo = false;
    }

    public enum EstadosDelProducto
    {
        Activo = 1,
        Inactivo = 2,
        Cancelado = 3
    }
}
