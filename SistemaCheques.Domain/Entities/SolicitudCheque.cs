using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaCheques.Domain.Enums;

namespace SistemaCheques.Domain.Entities;

public class SolicitudCheque
{
    public int Id { get; set; }
    
    [Required]
    public int ProveedorId { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Monto { get; set; }
    
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    
    public EstadoSolicitud Estado { get; set; } = EstadoSolicitud.Pendiente;
    
    [StringLength(50)]
    public string CuentaContableProveedor { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string CuentaContableBanco { get; set; } = string.Empty;
    
    [StringLength(20)]
    public string? NumeroCheque { get; set; }
    
    public DateTime? FechaGeneracionCheque { get; set; }
    
    public DateTime? FechaModificacion { get; set; }
    
    // Relaciones
    [ForeignKey("ProveedorId")]
    public virtual Proveedor Proveedor { get; set; } = null!;
} 