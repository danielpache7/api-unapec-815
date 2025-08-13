using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCheques.Application.DTOs;
using SistemaCheques.Application.Commands.Proveedor;
using SistemaCheques.Application.Queries.Proveedor;
using SistemaCheques.Domain.Enums;

namespace SistemaCheques.API.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize] // Temporalmente deshabilitado
public class ProveedoresController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProveedoresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene todos los proveedores
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProveedorDto>>> GetAll([FromQuery] bool soloActivos = false)
    {
        var query = new GetAllProveedoresQuery(soloActivos);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Obtiene un proveedor por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProveedorDto>> GetById(int id)
    {
        var query = new GetProveedorByIdQuery(id);
        var result = await _mediator.Send(query);
        if (result == null)
            return NotFound($"Proveedor con ID {id} no encontrado");
        return Ok(result);
    }

    /// <summary>
    /// Obtiene proveedores por tipo de persona
    /// </summary>
    [HttpGet("por-tipo/{tipoPersona}")]
    public async Task<ActionResult<IEnumerable<ProveedorDto>>> GetByTipoPersona(TipoPersona tipoPersona)
    {
        var query = new GetProveedoresByTipoPersonaQuery(tipoPersona);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Busca un proveedor por cédula/RNC
    /// </summary>
    [HttpGet("por-cedula/{cedulaRnc}")]
    public async Task<ActionResult<ProveedorDto>> GetByCedulaRnc(string cedulaRnc)
    {
        var query = new GetProveedorByCedulaRncQuery(cedulaRnc);
        var result = await _mediator.Send(query);
        if (result == null)
            return NotFound($"Proveedor con cédula/RNC {cedulaRnc} no encontrado");
        return Ok(result);
    }

    /// <summary>
    /// Crea un nuevo proveedor
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ProveedorDto>> Create([FromBody] CreateProveedorDto dto)
    {
        var command = new CreateProveedorCommand
        {
            Nombre = dto.Nombre,
            TipoPersona = dto.TipoPersona,
            CedulaRnc = dto.CedulaRnc,
            Balance = dto.Balance,
            CuentaContableProveedor = dto.CuentaContableProveedor,
            Estado = dto.Estado
        };
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Actualiza un proveedor existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ProveedorDto>> Update(int id, [FromBody] UpdateProveedorDto dto)
    {
        if (id != dto.Id)
            return BadRequest("El ID de la URL no coincide con el ID del DTO");

        var command = new UpdateProveedorCommand
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            TipoPersona = dto.TipoPersona,
            CedulaRnc = dto.CedulaRnc,
            Balance = dto.Balance,
            CuentaContableProveedor = dto.CuentaContableProveedor,
            Estado = dto.Estado
        };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Elimina un proveedor
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteProveedorCommand(id);
        var result = await _mediator.Send(command);
        if (!result)
            return NotFound($"Proveedor con ID {id} no encontrado");
        return NoContent();
    }
} 