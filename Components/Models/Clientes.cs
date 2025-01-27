using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models
{
    public class Clientes
    {

        [Key]
        public int ClienteId { get; set; }
  
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        public double LimiteCredito { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public String FechaIngreso { get; set; }

        [Required(ErrorMessage = "Debe añadir el RNC.")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "El Rnc debe contar con 9 dijitos")]
        public string? Rnc { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [ForeignKey("Tecnico")]
        public int TecnicoId { get; set; }

    }

}

