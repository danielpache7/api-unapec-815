using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCheques.Application.DTOs;
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
        // Implementar queries específicas para proveedores
        return Ok(new List<ProveedorDto>());
    }

    /// <summary>
    /// Obtiene un proveedor por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProveedorDto>> GetById(int id)
    {
        // Implementar query específica
        return Ok(new ProveedorDto());
    }

    /// <summary>
    /// Obtiene proveedores por tipo de persona
    /// </summary>
    [HttpGet("por-tipo/{tipoPersona}")]
    public async Task<ActionResult<IEnumerable<ProveedorDto>>> GetByTipoPersona(TipoPersona tipoPersona)
    {
        // Implementar query específica
        return Ok(new List<ProveedorDto>());
    }

    /// <summary>
    /// Busca un proveedor por cédula/RNC
    /// </summary>
    [HttpGet("por-cedula/{cedulaRnc}")]
    public async Task<ActionResult<ProveedorDto>> GetByCedulaRnc(string cedulaRnc)
    {
        // Implementar query específica
        return Ok(new ProveedorDto());
    }

    /// <summary>
    /// Crea un nuevo proveedor
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ProveedorDto>> Create([FromBody] CreateProveedorDto dto)
    {
        // Implementar command
        return CreatedAtAction(nameof(GetById), new { id = 1 }, new ProveedorDto());
    }

    /// <summary>
    /// Actualiza un proveedor existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ProveedorDto>> Update(int id, [FromBody] UpdateProveedorDto dto)
    {
        if (id != dto.Id)
            return BadRequest("El ID de la URL no coincide con el ID del DTO");

        // Implementar command
        return Ok(new ProveedorDto());
    }

    /// <summary>
    /// Elimina un proveedor
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        // Implementar command
        return NoContent();
    }
} 