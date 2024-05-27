using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTecnicos.Models;

public class TiposTecnicos
{
    [Key]
    public int TipoId { get; set; }

    [Required(ErrorMessage = "El campo descripción es requerido")]
    [RegularExpression(@"^[a-zA-ZñÑ\s]+$", ErrorMessage = "Este campo no debe contener caracteres especiales")]
    public string? Descripcion { get; set; }

    public decimal? Incentivo { get; set; }

    public Tecnicos? Tecnicos { get; set; }
}
