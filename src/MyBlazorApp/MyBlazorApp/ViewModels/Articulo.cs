using System.ComponentModel.DataAnnotations;

namespace MyBlazorApp.ViewModels //Candidato a biblioteca de clases propia
{
    public class Articulo
    {
        public DateTime FechaCreacion { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [Range(0, 99, ErrorMessage="Debe ser un identificador de dos digitos (00-99)")]
        public int Id { get; set; }

        public int codeId => 1000 + Id;

        [MaxLength(100)]
        [MinLength(4)]
        public string Descripcion { get; set; }

        [Range(typeof(decimal), "0.01", "1000000")]
        public decimal Precio { get; set; }
       
        public Articulo()
        {
            FechaCreacion = DateTime.Now;
            Id = 1;
            Descripcion = "Tornillo";
            Precio = 0.30m;
      }
    }
}

