using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCheques.Application.DTOs;
using SistemaCheques.Domain.Enums;

namespace SistemaCheques.API.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize] // Temporalmente deshabilitado
public class SolicitudesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SolicitudesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene todas las solicitudes con filtros opcionales
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SolicitudChequeDto>>> GetAll(
        [FromQuery] int? proveedorId = null,
        [FromQuery] EstadoSolicitud? estado = null,
        [FromQuery] DateTime? fechaInicio = null,
        [FromQuery] DateTime? fechaFin = null)
    {
        // Implementar query con filtros
        return Ok(new List<SolicitudChequeDto>());
    }

    /// <summary>
    /// Obtiene una solicitud por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<SolicitudChequeDto>> GetById(int id)
    {
        // Implementar query específica
        return Ok(new SolicitudChequeDto());
    }

    /// <summary>
    /// Obtiene solicitudes pendientes
    /// </summary>
    [HttpGet("pendientes")]
    public async Task<ActionResult<IEnumerable<SolicitudChequeDto>>> GetPendientes()
    {
        // Implementar query específica
        return Ok(new List<SolicitudChequeDto>());
    }

    /// <summary>
    /// Crea una nueva solicitud de cheque
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<SolicitudChequeDto>> Create([FromBody] CreateSolicitudChequeDto dto)
    {
        // Implementar command
        return CreatedAtAction(nameof(GetById), new { id = 1 }, new SolicitudChequeDto());
    }

    /// <summary>
    /// Actualiza una solicitud de cheque
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<SolicitudChequeDto>> Update(int id, [FromBody] UpdateSolicitudChequeDto dto)
    {
        if (id != dto.Id)
            return BadRequest("El ID de la URL no coincide con el ID del DTO");

        // Implementar command
        return Ok(new SolicitudChequeDto());
    }

    /// <summary>
    /// Genera un cheque para una solicitud
    /// </summary>
    [HttpPost("{id}/generar-cheque")]
    public async Task<ActionResult<SolicitudChequeDto>> GenerarCheque(int id, [FromBody] GenerarChequeDto dto)
    {
        if (id != dto.SolicitudId)
            return BadRequest("El ID de la URL no coincide con el ID del DTO");

        // Implementar command para generar cheque
        return Ok(new SolicitudChequeDto());
    }

    /// <summary>
    /// Anula una solicitud de cheque
    /// </summary>
    [HttpPost("{id}/anular")]
    public async Task<ActionResult<SolicitudChequeDto>> Anular(int id)
    {
        // Implementar command para anular
        return Ok(new SolicitudChequeDto());
    }
} 