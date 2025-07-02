using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCheques.Application.DTOs;

namespace SistemaCheques.API.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize] // Temporalmente deshabilitado
public class AsientosController : ControllerBase
{
    private readonly IMediator _mediator;

    public AsientosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene todos los asientos contables
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AsientoContableDto>>> GetAll()
    {
        // Implementar query
        return Ok(new List<AsientoContableDto>());
    }

    /// <summary>
    /// Obtiene asientos contables por enviar
    /// </summary>
    [HttpGet("por-enviar")]
    public async Task<ActionResult<IEnumerable<AsientoContableDto>>> GetPorEnviar()
    {
        // Implementar query específica
        return Ok(new List<AsientoContableDto>());
    }

    /// <summary>
    /// Obtiene asientos contables por fecha
    /// </summary>
    [HttpGet("por-fecha")]
    public async Task<ActionResult<IEnumerable<AsientoContableDto>>> GetByFecha([FromQuery] DateTime fecha)
    {
        // Implementar query específica
        return Ok(new List<AsientoContableDto>());
    }

    /// <summary>
    /// Genera asiento contable sumando montos por cuenta desde solicitudes del mes
    /// </summary>
    [HttpPost("generar")]
    public async Task<ActionResult<AsientoContableDto>> GenerarAsiento([FromBody] GenerarAsientoContableDto dto)
    {
        // Implementar command para generar asiento
        // Debe sumar montos por cuenta contable de las solicitudes del mes especificado
        // Luego enviar al servicio web de contabilidad
        return Ok(new AsientoContableDto());
    }

    /// <summary>
    /// Envía un asiento contable al sistema de contabilidad
    /// </summary>
    [HttpPost("{id}/enviar")]
    public async Task<ActionResult<AsientoContableDto>> EnviarAsiento(int id)
    {
        // Implementar command para enviar asiento al servicio web
        return Ok(new AsientoContableDto());
    }
} 