using System.ComponentModel.DataAnnotations;

namespace SistemaCheques.Domain.Entities;

public class ConceptoPago
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Descripcion { get; set; } = string.Empty;
    
    public bool Estado { get; set; } = true;
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    
    public DateTime? FechaModificacion { get; set; }
} 