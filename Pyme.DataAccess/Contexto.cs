using Pyme.DataAccess.Modelos;
using System;
using System.Data.Entity;

namespace Pyme.DataAccess
{
    public class Contexto : DbContext
    {
        // Se asigna en Program.cs del proyecto PymeCo
        public static string DefaultConnectionString { get; set; }

        // Úsalo cuando registres el contexto vía DI (opcional)
        public Contexto(string connectionString) : base(connectionString) { }

        // Compatibilidad con tu código actual (new Contexto())
        public Contexto()
            : this(DefaultConnectionString ??
                   throw new InvalidOperationException("Contexto.DefaultConnectionString no está configurada."))
        { }

        public DbSet<ProductoAD> Producto { get; set; }
    }
}
