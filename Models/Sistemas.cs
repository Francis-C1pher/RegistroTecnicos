using System.ComponentModel.DataAnnotations;
namespace RegistroTecnicos.Models

{
    public class Sistemas
    {

        [Key]
        public int SistemaId { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Complejidad { get; set; } = string.Empty;
    }
}
