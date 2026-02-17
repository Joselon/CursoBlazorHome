using System.ComponentModel.DataAnnotations;

namespace MyBlazorApp.ViewModels //Candidato a biblioteca de clases propia
{
    public class TarjetaPregunta
    {
        public DateTime FechaCreacion { get; set; }
        public int Id { get; set; }
        [Range(0,99)] 
        public int internalId => 1000 + Id;
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
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
