using System.ComponentModel.DataAnnotations;

namespace ProyectoTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }

    [Required(ErrorMessage ="El campo Nombres es olbigatorio!")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El Nombre debe contener solo letras.")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage ="El campo Sueldo es obligatorio!")]
    [Range(0.01, float.MaxValue, ErrorMessage ="Ingrese un valor mayor a 0")]
    public decimal? SueldoHora { get; set; }
}
