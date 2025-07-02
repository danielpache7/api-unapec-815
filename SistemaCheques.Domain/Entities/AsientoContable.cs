using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCheques.Domain.Entities;

public class AsientoContable
{
    public int Id { get; set; }
    
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    
    [Required]
    [StringLength(50)]
    public string CuentaContable { get; set; } = string.Empty;
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal MontoTotal { get; set; }
    
    [StringLength(500)]
    public string? Descripcion { get; set; }
    
    [StringLength(20)]
    public string? NumeroAsiento { get; set; }
    
    public bool Enviado { get; set; } = false;
    
    public DateTime? FechaEnvio { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
} 