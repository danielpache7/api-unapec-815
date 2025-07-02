using SistemaCheques.Domain.Enums;

namespace SistemaCheques.Application.DTOs;

public class SolicitudChequeDto
{
    public int Id { get; set; }
    public int ProveedorId { get; set; }
    public string ProveedorNombre { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public DateTime FechaRegistro { get; set; }
    public EstadoSolicitud Estado { get; set; }
    public string EstadoDescripcion => Estado.ToString();
    public string CuentaContableProveedor { get; set; } = string.Empty;
    public string CuentaContableBanco { get; set; } = string.Empty;
    public string? NumeroCheque { get; set; }
    public DateTime? FechaGeneracionCheque { get; set; }
    public DateTime? FechaModificacion { get; set; }
}

public class CreateSolicitudChequeDto
{
    public int ProveedorId { get; set; }
    public decimal Monto { get; set; }
    public string CuentaContableProveedor { get; set; } = string.Empty;
    public string CuentaContableBanco { get; set; } = string.Empty;
}

public class UpdateSolicitudChequeDto
{
    public int Id { get; set; }
    public decimal Monto { get; set; }
    public string CuentaContableProveedor { get; set; } = string.Empty;
    public string CuentaContableBanco { get; set; } = string.Empty;
}

public class GenerarChequeDto
{
    public int SolicitudId { get; set; }
    public string NumeroCheque { get; set; } = string.Empty;
} 