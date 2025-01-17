using System.ComponentModel.DataAnnotations;
namespace RegistroTecnicos.Components.Models
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Concepto { get; set; } = null!;
    }
}
