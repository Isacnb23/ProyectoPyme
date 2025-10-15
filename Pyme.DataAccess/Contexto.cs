using Pyme.DataAccess.Modelos;
using System.Data.Entity;

namespace Pyme.DataAccess
{
    public class Contexto : DbContext
    {
        // Looks for a connection string named "Pyme" in the *web* project's Web.config
        public Contexto() : base("name=Pyme") { }

        public DbSet<ProductoAD> Producto { get; set; }
    }
}
