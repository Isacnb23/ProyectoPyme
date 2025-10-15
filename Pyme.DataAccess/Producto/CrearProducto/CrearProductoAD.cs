using Pyme.Abstracciones.AccesoADatos.Producto.CrearProducto;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.AccesoADatos;
using Pyme.DataAccess.Modelos;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyme.DataAccess.Producto.CrearProducto
{
    public class CrearProductoAD : ICrearProductoAD
    {
        private Contexto _contexto;

        public CrearProductoAD()
        {
            _contexto = new Contexto();
        }

        public async Task<int> Guardar(ProductoDto elProducto)
        {
            //_contexto.Database.ExecuteSqlCommand("EXEC PROC @elParametro, @elParametro2", "", "");
            ProductoAD elProductoAGuardar = ConvertirObjetoParaAD(elProducto);

            _contexto.Producto.Add(elProductoAGuardar);

            EntityState estado = _contexto.Entry(elProductoAGuardar).State = System.Data.Entity.EntityState.Added;
            int cantidadDeDatosAgregados = await _contexto.SaveChangesAsync();
            return cantidadDeDatosAgregados;
        }

        // {1,2,3,4}

        private ProductoAD ConvertirObjetoParaAD(ProductoDto producto)
        {
            return new ProductoAD
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                CategoriaId = producto.CategoriaId,
                Precio = producto.Precio,
                ImpuestoPorc = producto.ImpuestoPorc,
                Stock = producto.Stock,
                ImagenUrl = producto.ImagenUrl,
                EstadoProducto = producto.EstadoProducto
            };
        }

    }
}
