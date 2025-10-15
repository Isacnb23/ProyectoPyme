using Pyme.Abstracciones.AccesoADatos.Producto.ListarProducto;
using Pyme.Abstracciones.LogicaDeNegocio.Producto.ListarProducto;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.DataAccess;
using Pyme.DataAccess.Modelos;
using System.Collections.Generic;
using System.Linq;

namespace Pyme.DataAccess.Producto.ListarProducto
{
    public class ListarProductoAD : IListarProductoAD
    {
        private Contexto _elContexto;

        public ListarProductoAD()
        {
            _elContexto = new Contexto();
        }

        public List<ProductoDto> Obtener()
        {
            //List<ProductoAD> laListaEnBaseDeDatos = _elContexto.Producto.ToList();

            List<ProductoDto> laListaARetornar = (
                from producto in _elContexto.Producto
                select new ProductoDto
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    CategoriaId = producto.CategoriaId,
                    Precio = producto.Precio,
                    ImpuestoPorc = producto.ImpuestoPorc,
                    Stock = producto.Stock,
                    EstadoProducto = producto.EstadoProducto
                }

            ).ToList();

            return laListaARetornar;
        }
    }
}
