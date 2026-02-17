using System.ComponentModel.DataAnnotations;

namespace MyBlazorApp.ViewModels //Candidato a biblioteca de clases propia
{
    public class TarjetaPregunta
    {
        public DateTime FechaCreacion { get; set; }
        [Range(0, 99)]
        public int Id { get; set; }

        public int codeId => 1000 + Id;
        [MaxLength(100)]
        [MinLength(4)]
        public string Pregunta { get; set; }
        [MinLength(1)]
        [MaxLength(100)]
        public string Respuesta { get; set; }
        [MinLength(4)]
        [MaxLength(100)]
        public string Tema { get; set; }
        

        public TarjetaPregunta()
        {
            FechaCreacion = DateTime.Now;
            Id = 1;
            Pregunta = "2 + 2";
            Respuesta = "4";
            Tema = "Matemáticas";
        }
    }


}
