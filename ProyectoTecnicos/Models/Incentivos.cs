using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTecnicos.Models;

public class Incentivos
{
    [Key]
    public int IncentivoId { get; set; }

    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "El campo descripción es requerido")]
    [RegularExpression(@"^[a-zA-ZñÑ\s]+$", ErrorMessage = "Este campo no debe contener caracteres especiales")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El campo CantidadServicios es requerido")]
    public int CantidadServicios { get; set; }

    [Required(ErrorMessage = "El campo Monto es requerido")]
    [Range(0.01, float.MaxValue, ErrorMessage = "Ingrese un valor mayor a 0")]
    public decimal Monto { get; set; }

    [ForeignKey("Tecnicos")]
    public int TecnicoId {  get; set; }
    public Tecnicos? Tecnicos { get; set; }
}
