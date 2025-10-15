using Pyme.Abstracciones.AccesoADatos.Producto.ObtenerProductoPorId;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.DataAccess.Modelos;
using Pyme.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.AccesoADatos.Producto.ObtenerProductoPorId
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
            ProductoDto laListaARetornar = (from Producto in _elContexto.Producto
                                              where Producto.Id == id
                                              select new ProductoDto
                                              {
                                                  Id = Producto.Id,
                                                  Nombre = Producto.Nombre,
                                                  CategoriaId = Producto.CategoriaId,
                                                  Precio = Producto.Precio,
                                                  ImpuestoPorc = Producto.ImpuestoPorc,
                                                  Stock = Producto.Stock,
                                                  EstadoProducto = Producto.EstadoProducto
                                              }).FirstOrDefault();
            return laListaARetornar;
        }
    }
}
