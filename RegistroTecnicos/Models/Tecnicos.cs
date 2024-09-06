using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Tecnicos
    {
        [Key]
        public int TecnicosId { get; set;  }
        [Required(ErrorMessage = "El Campo Descri&oacute;n es obligatorio")]
        public string? TecnicosName { get; set; }
    }
}
