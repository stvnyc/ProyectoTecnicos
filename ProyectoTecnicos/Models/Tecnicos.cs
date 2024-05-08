using System.ComponentModel.DataAnnotations;

namespace ProyectoTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }
    [Required(ErrorMessage ="El campo Nombres es olbigatorio")]
    public string? Nombres { get; set; }
    [Required(ErrorMessage ="El campo Sueldo es obligatorio")]
    public decimal? SueldoHora { get; set; }
}
