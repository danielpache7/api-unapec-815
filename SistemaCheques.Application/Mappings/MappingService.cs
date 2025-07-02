using SistemaCheques.Application.DTOs;
using SistemaCheques.Domain.Entities;

namespace SistemaCheques.Application.Mappings;

public static class MappingService
{
    // ConceptoPago mappings
    public static ConceptoPagoDto ToDto(this ConceptoPago entity)
    {
        return new ConceptoPagoDto
        {
            Id = entity.Id,
            Descripcion = entity.Descripcion,
            Estado = entity.Estado,
            FechaCreacion = entity.FechaCreacion,
            FechaModificacion = entity.FechaModificacion
        };
    }

    public static ConceptoPago ToEntity(this CreateConceptoPagoDto dto)
    {
        return new ConceptoPago
        {
            Descripcion = dto.Descripcion,
            Estado = dto.Estado,
            FechaCreacion = DateTime.UtcNow
        };
    }

    public static void UpdateEntity(this UpdateConceptoPagoDto dto, ConceptoPago entity)
    {
        entity.Descripcion = dto.Descripcion;
        entity.Estado = dto.Estado;
        entity.FechaModificacion = DateTime.UtcNow;
    }

    // Proveedor mappings
    public static ProveedorDto ToDto(this Proveedor entity)
    {
        return new ProveedorDto
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            TipoPersona = entity.TipoPersona,
            CedulaRnc = entity.CedulaRnc,
            Balance = entity.Balance,
            CuentaContableProveedor = entity.CuentaContableProveedor,
            Estado = entity.Estado,
            FechaCreacion = entity.FechaCreacion,
            FechaModificacion = entity.FechaModificacion
        };
    }

    public static Proveedor ToEntity(this CreateProveedorDto dto)
    {
        return new Proveedor
        {
            Nombre = dto.Nombre,
            TipoPersona = dto.TipoPersona,
            CedulaRnc = dto.CedulaRnc,
            Balance = dto.Balance,
            CuentaContableProveedor = dto.CuentaContableProveedor,
            Estado = dto.Estado,
            FechaCreacion = DateTime.UtcNow
        };
    }

    // SolicitudCheque mappings
    public static SolicitudChequeDto ToDto(this SolicitudCheque entity)
    {
        return new SolicitudChequeDto
        {
            Id = entity.Id,
            ProveedorId = entity.ProveedorId,
            ProveedorNombre = entity.Proveedor?.Nombre ?? string.Empty,
            Monto = entity.Monto,
            FechaRegistro = entity.FechaRegistro,
            Estado = entity.Estado,
            CuentaContableProveedor = entity.CuentaContableProveedor,
            CuentaContableBanco = entity.CuentaContableBanco,
            NumeroCheque = entity.NumeroCheque,
            FechaGeneracionCheque = entity.FechaGeneracionCheque,
            FechaModificacion = entity.FechaModificacion
        };
    }

    public static SolicitudCheque ToEntity(this CreateSolicitudChequeDto dto)
    {
        return new SolicitudCheque
        {
            ProveedorId = dto.ProveedorId,
            Monto = dto.Monto,
            CuentaContableProveedor = dto.CuentaContableProveedor,
            CuentaContableBanco = dto.CuentaContableBanco,
            FechaRegistro = DateTime.UtcNow
        };
    }

    // AsientoContable mappings
    public static AsientoContableDto ToDto(this AsientoContable entity)
    {
        return new AsientoContableDto
        {
            Id = entity.Id,
            Fecha = entity.Fecha,
            CuentaContable = entity.CuentaContable,
            MontoTotal = entity.MontoTotal,
            Descripcion = entity.Descripcion,
            NumeroAsiento = entity.NumeroAsiento,
            Enviado = entity.Enviado,
            FechaEnvio = entity.FechaEnvio,
            FechaCreacion = entity.FechaCreacion
        };
    }

    public static AsientoContable ToEntity(this CreateAsientoContableDto dto)
    {
        return new AsientoContable
        {
            Fecha = dto.Fecha,
            CuentaContable = dto.CuentaContable,
            MontoTotal = dto.MontoTotal,
            Descripcion = dto.Descripcion,
            NumeroAsiento = dto.NumeroAsiento,
            FechaCreacion = DateTime.UtcNow
        };
    }
} 