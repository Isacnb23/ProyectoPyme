using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pyme.DataAccess.Modelos
{
    [Table("Producto", Schema = "dbo")]
    public class ProductoAD
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("CategoriaId")]
        public int CategoriaId { get; set; }

        [Column("Precio")]
        public decimal Precio { get; set; }

        [Column("ImpuestoPorc")]
        public decimal ImpuestoPorc { get; set; }

        [Column("Stock")]
        public int Stock { get; set; }

        [Column("ImagenUrl")]
        public string ImagenUrl { get; set; }

        [Column("EstadoProducto")]
        public string EstadoProductoDb { get; set; }

        [NotMapped]
        public bool EstadoProducto
        {
            get
            {
                var v = (EstadoProductoDb ?? "").Trim().ToLower();
                return v == "activo" || v == "1" || v == "true";
            }
            set => EstadoProductoDb = value ? "Activo" : "Inactivo";
        }

        [Column("FechaRegistro")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime FechaDeRegistro { get; set; }

        [Column("FechaModificacion")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? FechaDeModificacion { get; set; }
    }
}
