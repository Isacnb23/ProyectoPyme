using System;
using System.Threading.Tasks;
using Pyme.Abstracciones.AccesoADatos.Producto.CrearProducto;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.DataAccess.Modelos;

namespace Pyme.DataAccess.Producto.CrearProducto
{
    public class CrearProductoAD : ICrearProductoAD
    {
        public async Task<int> Guardar(ProductoDto dto)
        {
            using (var ctx = new Contexto())
            {
                var categoriaId = (dto.CategoriaId > 0) ? dto.CategoriaId : 1;

                var entidad = new ProductoAD
                {
                    
                    Nombre = dto.Nombre?.Trim(),
                    CategoriaId = categoriaId,
                    Precio = dto.Precio,
                    ImpuestoPorc = dto.ImpuestoPorc,
                    Stock = dto.Stock,
                    ImagenUrl = NormalizarUrl(dto.ImagenUrl),

                    EstadoProductoDb = dto.EstadoProducto ? "Activo" : "Inactivo",

                    
                };

                ctx.Producto.Add(entidad);
                return await ctx.SaveChangesAsync();
            }
        }

        private static string NormalizarUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;
            url = url.Trim();
            return url.Length > 255 ? url.Substring(0, 255) : url;
        }
    }
}
