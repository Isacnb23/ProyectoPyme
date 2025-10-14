using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace Pyme.Abstracciones.ModelosParaUI
{
    public class ProductoDto
    {
        public int Id { get; set; }
        [DisplayName("Nombre del producto")]
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(150, MinimumLength = 2)]
        public string Nombre { get; set; }

        [DisplayName("Categoría")]
        [Required(ErrorMessage = "La categoría es requerida")]
        public int CategoriaId { get; set; }

        [DisplayName("Precio")]
        [Range(0, 999999.99, ErrorMessage = "El precio debe ser mayor o igual a 0")]
        public decimal Precio { get; set; }

        [DisplayName("% Impuesto")]
        [Range(0, 100, ErrorMessage = "El impuesto debe estar entre 0 y 100")]
        public decimal ImpuestoPorc { get; set; }

        [DisplayName("Stock")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")]
        public int Stock { get; set; }

        [DisplayName("URL de imagen")]
        [StringLength(255)]
        public string ImagenUrl { get; set; }

        [DisplayName("Estado")]
        [Required]
        public EstadosDelProducto EstadoProducto { get; set; }


        [DisplayName("Archivo de imagen")]
        public HttpPostedFileBase ArchivoImagen { get; set; }

    }
}
