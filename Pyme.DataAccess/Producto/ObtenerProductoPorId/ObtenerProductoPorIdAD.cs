using Pyme.Abstracciones.AccesoADatos.Producto.ObtenerProductoPorId;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.DataAccess.Modelos;
using Pyme.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyme.DataAccess.Producto.ObtenerProductoPorId
{
    public class ObtenerProductoPorIdAD : IObtenerProductoPorIdAD
    {
        private Contexto _elContexto;
        public ObtenerProductoPorIdAD()
        {
            _elContexto = new Contexto();
        }
        public ProductoDto Obtener(int id)
        {
            var entidad = _elContexto.Producto.FirstOrDefault(p => p.Id == id);

            if (entidad == null)
                return null;

            return new ProductoDto
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre,
                CategoriaId = entidad.CategoriaId,
                Precio = entidad.Precio,
                ImpuestoPorc = entidad.ImpuestoPorc,
                Stock = entidad.Stock,
                ImagenUrl = entidad.ImagenUrl,
                EstadoProducto = entidad.EstadoProducto
            };
        }

    }
}
