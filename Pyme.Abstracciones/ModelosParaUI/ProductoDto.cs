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
        [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres.")]
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string Nombre { get; set; }

        [DisplayName("Categoría")]
        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        public int CategoriaId { get; set; }

        [DisplayName("Nombre de la categoría")]
        public string CategoriaNombre { get; set; }

        [DisplayName("Precio")]
        [Range(0, 9999999999.99, ErrorMessage = "El precio no puede ser negativo.")]
        [Required(ErrorMessage = "El precio es obligatorio.")]
        public decimal Precio { get; set; }

        [DisplayName("Impuesto (%)")]
        [Range(0, 100, ErrorMessage = "El impuesto debe estar entre 0 y 100.")]
        [Required(ErrorMessage = "Debe indicar el porcentaje de impuesto.")]
        public decimal ImpuestoPorc { get; set; }

        [DisplayName("Stock disponible")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [DisplayName("Imagen del producto")]
        public string ImagenUrl { get; set; }

        [DisplayName("Estado del producto")]
        [Required(ErrorMessage = "Tiene que elegir un estado del producto")]
        [Display(Name = "Activo")]
        public bool EstadoProducto { get; set; } = true;

        [DisplayName("Fecha de registro")]
        public DateTime FechaRegistro { get; set; }

        [DisplayName("Fecha de modificación")]
        public DateTime? FechaModificacion { get; set; }

        [DisplayName("Archivo de imagen")]
        public HttpPostedFileBase archivo { get; set; }
    }
}
