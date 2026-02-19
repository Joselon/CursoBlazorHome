using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlazorApp.ViewModels //Candidato a biblioteca de clases propia
{
    [Table("alm_mar")]
    public class Articulo
    {

        [Required(ErrorMessage = "Campo obligatorio")]
        [Range(0, 99, ErrorMessage="Debe ser un identificador de dos digitos (00-99)")]
        [Column("mar_codigo")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100)]
        [MinLength(4)]
        [Column("mar_descripcion")]
        public string Descripcion { get; set; }

        [Range(typeof(decimal), "0.01", "1000000")]
        [Column("mar_precio_1")]
        public decimal Precio { get; set; }
       
    }
}

