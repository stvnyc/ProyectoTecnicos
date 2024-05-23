using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }

    [Required(ErrorMessage = "El campo Nombres es olbigatorio!")]
    [RegularExpression(@"^[a-zA-ZñÑ\s]+$", ErrorMessage = "El Nombre debe contener solo letras.")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "El campo Sueldo es obligatorio!")]
    [Range(0.01, float.MaxValue, ErrorMessage = "Ingrese un valor mayor a 0")]
    public decimal? SueldoHora { get; set; }

    [ForeignKey("TiposTecnicos")]
    [Range(1, 100, ErrorMessage = "Selecciona un tipo")]
    public int idTipo { get; set; }

    public TiposTecnicos? TiposTecnicos { get; set; }
}
