using Pyme.DataAccess.Modelos;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace Pyme.AccesoADatos
{
    public class Contexto : DbContext
    {
        public Contexto() : base("name=Contexto")
        {
        }

        public DbSet<ProductoAD> Producto { get; set; }
    }
}
