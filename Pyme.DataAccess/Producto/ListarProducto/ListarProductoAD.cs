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

        //public List<ProductoDto> Obtener()
        //{
        //    List<ProductoAD> laListaEnBaseDeDatos = _elContexto.Producto.ToList();

        //    List<ProductoDto> laListaARetornar = (
        //        from producto in _elContexto.Producto
        //        select new ProductoDto
        //        {
        //            Id = producto.Id,
        //            Nombre = producto.Nombre,
        //            CategoriaId = producto.CategoriaId,
        //            Precio = producto.Precio,
        //            ImpuestoPorc = producto.ImpuestoPorc,
        //            Stock = producto.Stock,


        //            EstadoProducto = producto.EstadoProducto
        //        }

        //    ).ToList();

        //    return laListaARetornar;
        //}
        public List<ProductoDto> Obtener()
        {
            var lista = _elContexto.Producto
                // 1) Solo columnas reales (traducibles a SQL)
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    p.CategoriaId,
                    p.Precio,
                    p.ImpuestoPorc,
                    p.Stock,
                    p.ImagenUrl,
                    Estado = p.EstadoProductoDb,       // <-- columna VARCHAR de BD
                    p.FechaDeRegistro,
                    p.FechaDeModificacion
                })
                .AsEnumerable() // <-- de aquí en adelante es LINQ to Objects
                                // 2) Ya en memoria, convertimos a tu DTO con el bool
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    CategoriaId = p.CategoriaId,
                    Precio = p.Precio,
                    ImpuestoPorc = p.ImpuestoPorc,
                    Stock = p.Stock,
                    ImagenUrl = p.ImagenUrl,
                    EstadoProducto = ToBoolEstado(p.Estado),
                    FechaRegistro = p.FechaDeRegistro,
                    FechaModificacion = p.FechaDeModificacion
                })
                .ToList();

            return lista;
        }

        private static bool ToBoolEstado(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;
            var v = s.Trim().ToLower();
            return v == "activo" || v == "1" || v == "true";
        }
    }
}
