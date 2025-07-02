using System.ComponentModel.DataAnnotations;
using SistemaCheques.Domain.Enums;

namespace SistemaCheques.Domain.Entities;

public class Proveedor
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Nombre { get; set; } = string.Empty;
    
    public TipoPersona TipoPersona { get; set; }
    
    [Required]
    [StringLength(20)]
    public string CedulaRnc { get; set; } = string.Empty;
    
    public decimal Balance { get; set; }
    
    [StringLength(50)]
    public string CuentaContableProveedor { get; set; } = string.Empty;
    
    public bool Estado { get; set; } = true;
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    
    public DateTime? FechaModificacion { get; set; }
    
    // Relaciones
    public virtual ICollection<SolicitudCheque> SolicitudesCheques { get; set; } = new List<SolicitudCheque>();
} 