//using Pyme.Abstracciones.AccesoADatos.Producto.ActualizarProducto;
//using Pyme.Abstracciones.ModelosParaUI;
//using Pyme.DataAccess;
//using Pyme.DataAccess.Modelos;
//using System.Data.Entity;
//using System.Linq;

//namespace Pyme.DataAccess.Producto.ActualizarProducto
//{
//    public class ActualizarProductoAD : IActualizarProductoAD
//    {
//        private Contexto _contexto;
//        public ActualizarProductoAD()
//        {
//            _contexto = new Contexto();
//        }

//        public int Actualizar(ProductoDto elProducto)
//        {
//            ProductoAD elProductoEnBaseDeDatos = _contexto.Producto.Where(Producto => Producto.Id == elProducto.Id).FirstOrDefault();

//            // Actualiza campos editables
//            elProductoEnBaseDeDatos.Id = elProducto.Id;
//            elProductoEnBaseDeDatos.Nombre = elProducto.Nombre;
//            elProductoEnBaseDeDatos.CategoriaId = elProducto.CategoriaId;
//            elProductoEnBaseDeDatos.Precio = elProducto.Precio;
//            elProductoEnBaseDeDatos.ImpuestoPorc = elProducto.ImpuestoPorc;
//            elProductoEnBaseDeDatos.Stock = elProducto.Stock;
//            EntityState estado = _contexto.Entry(elProductoEnBaseDeDatos).State = System.Data.Entity.EntityState.Modified;
//            int cantidadDeDatosAgregados = _contexto.SaveChanges();
//            return cantidadDeDatosAgregados;
//        }
//    }
//}


using Pyme.Abstracciones.AccesoADatos.Producto.ActualizarProducto;
using Pyme.Abstracciones.ModelosParaUI;
using Pyme.DataAccess.Modelos;
using System.Data.Entity;
using System.Linq;

namespace Pyme.DataAccess.Producto.ActualizarProducto
{
    public class ActualizarProductoAD : IActualizarProductoAD
    {
        private readonly Contexto _contexto;

        public ActualizarProductoAD()
        {
            _contexto = new Contexto();
        }

        public int Actualizar(ProductoDto elProducto)
        {
            // 1) Buscar entidad
            var entidad = _contexto.Producto.FirstOrDefault(p => p.Id == elProducto.Id);
            if (entidad == null) return 0;

            // 2) Mapear campos editables (Opción A: materializado → asignación → Modified)
            entidad.Nombre = (elProducto.Nombre ?? string.Empty).Trim();
            entidad.CategoriaId = elProducto.CategoriaId;
            entidad.Precio = elProducto.Precio;
            entidad.ImpuestoPorc = elProducto.ImpuestoPorc;
            entidad.Stock = elProducto.Stock;

            // Normaliza URL (igual que en Crear)
            entidad.ImagenUrl = (elProducto.ImagenUrl ?? string.Empty).Trim();

            // ⚠️ Mapeo bool → VARCHAR
            // Si tu modelo AD tiene la propiedad string mapeada: EstadoProductoDb
            entidad.EstadoProductoDb = elProducto.EstadoProducto ? "Activo" : "Inactivo";

            // 3) Marcar como modificado y guardar
            _contexto.Entry(entidad).State = EntityState.Modified;
            var afectados = _contexto.SaveChanges();

            return afectados;
        }
    }
}
