using System.ComponentModel.DataAnnotations;

namespace ProyectoTecnicos.Models;

public class TiposTecnicos
{
    [Key]
    public int TipoId { get; set; }

    [Required(ErrorMessage = "El campo descripción es requerido")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Este campo no debe contener caracteres especiales")]
    public string? Descripcion { get; set; }
}
