using MediatR;
using SistemaCheques.Application.DTOs;
using SistemaCheques.Domain.Enums;

namespace SistemaCheques.Application.Commands.Proveedor;

public class CreateProveedorCommand : IRequest<ProveedorDto>
{
    public string Nombre { get; set; } = string.Empty;
    public TipoPersona TipoPersona { get; set; }
    public string CedulaRnc { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string CuentaContableProveedor { get; set; } = string.Empty;
    public bool Estado { get; set; } = true;
} 