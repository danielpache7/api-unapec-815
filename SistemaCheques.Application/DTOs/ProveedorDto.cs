using SistemaCheques.Domain.Enums;

namespace SistemaCheques.Application.DTOs;

public class ProveedorDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public TipoPersona TipoPersona { get; set; }
    public string TipoPersonaDescripcion => TipoPersona.ToString();
    public string CedulaRnc { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string CuentaContableProveedor { get; set; } = string.Empty;
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
}

public class CreateProveedorDto
{
    public string Nombre { get; set; } = string.Empty;
    public TipoPersona TipoPersona { get; set; }
    public string CedulaRnc { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string CuentaContableProveedor { get; set; } = string.Empty;
    public bool Estado { get; set; } = true;
}

public class UpdateProveedorDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public TipoPersona TipoPersona { get; set; }
    public string CedulaRnc { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string CuentaContableProveedor { get; set; } = string.Empty;
    public bool Estado { get; set; }
} 