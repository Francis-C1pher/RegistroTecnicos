using System.ComponentModel.DataAnnotations;
namespace RegistroTecnicos.Components.Models

{
    public class Sistemas
    {

        [Key]
        public int SistemaId { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Complegidad { get; set; } = string.Empty;
    }
}
