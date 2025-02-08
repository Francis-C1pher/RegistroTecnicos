using System.ComponentModel.DataAnnotations;
namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public string Nombres { get; set; } = string.Empty;
    public double SueldoHora {  get; set; }
}
