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
            // Un contexto por operación
            using (var ctx = new Contexto())
            {
                // Asegura FK NOT NULL (1 y 2 existen en tu BD: Bebidas / Snacks)
                var categoriaId = (dto.CategoriaId > 0) ? dto.CategoriaId : 1;

                var entidad = new ProductoAD
                {
                    // NO asignes Id (es identidad)
                    Nombre = dto.Nombre?.Trim(),
                    CategoriaId = categoriaId,
                    Precio = dto.Precio,
                    ImpuestoPorc = dto.ImpuestoPorc,
                    Stock = dto.Stock,
                    ImagenUrl = NormalizarUrl(dto.ImagenUrl),

                    // clave: mapear al campo real de BD (varchar)
                    EstadoProductoDb = dto.EstadoProducto ? "Activo" : "Inactivo",

                    // Fechas las pone el trigger → no asignar aquí
                };

                ctx.Producto.Add(entidad);
                return await ctx.SaveChangesAsync();
            }
        }

        // Recorta/normaliza la URL a lo que soporte la columna (255 típico)
        private static string NormalizarUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;
            url = url.Trim();
            return url.Length > 255 ? url.Substring(0, 255) : url;
        }
    }
}
