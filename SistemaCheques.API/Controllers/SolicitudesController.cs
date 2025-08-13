using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaCheques.Application.Commands.SolicitudCheque;
using SistemaCheques.Application.DTOs;
using SistemaCheques.Application.Queries.SolicitudCheque;
using SistemaCheques.Domain.Enums;

namespace SistemaCheques.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        [FromQuery] DateTime? fechaFin = null,
        [FromQuery] decimal? montoMinimo = null,
        [FromQuery] decimal? montoMaximo = null,
        [FromQuery] string? cuentaContable = null,
        [FromQuery] bool? tieneChequeGenerado = null)
    {
        try
        {
            var query = new GetSolicitudesConFiltrosQuery
            {
                ProveedorId = proveedorId,
                Estado = estado,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                MontoMinimo = montoMinimo,
                MontoMaximo = montoMaximo,
                CuentaContable = cuentaContable,
                TieneChequeGenerado = tieneChequeGenerado
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener solicitudes: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtiene una solicitud por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<SolicitudChequeDto>> GetById(int id)
    {
        try
        {
            var query = new GetSolicitudByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            
            if (result == null)
                return NotFound($"Solicitud con ID {id} no encontrada");

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener solicitud: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtiene solicitudes pendientes
    /// </summary>
    [HttpGet("pendientes")]
    public async Task<ActionResult<IEnumerable<SolicitudChequeDto>>> GetPendientes()
    {
        try
        {
            var query = new GetSolicitudesPendientesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener solicitudes pendientes: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtiene solicitudes que se pueden anular (pendientes y con cheque generado)
    /// </summary>
    [HttpGet("anulables")]
    public async Task<ActionResult<IEnumerable<SolicitudChequeDto>>> GetAnulables()
    {
        try
        {
            var query = new GetSolicitudesConFiltrosQuery
            {
                Estado = null // Obtener todas
            };
            var todasLasSolicitudes = await _mediator.Send(query);
            
            // Filtrar solo las que se pueden anular (excluir las ya anuladas)
            var solicitudesAnulables = todasLasSolicitudes
                .Where(s => s.Estado != EstadoSolicitud.Anulada)
                .OrderByDescending(s => s.FechaRegistro);
            
            return Ok(solicitudesAnulables);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener solicitudes anulables: {ex.Message}");
        }
    }

    /// <summary>
    /// Crea una nueva solicitud de cheque
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<SolicitudChequeDto>> Create([FromBody] CreateSolicitudChequeDto dto)
    {
        try
        {
            var command = new CreateSolicitudChequeCommand
            {
                ProveedorId = dto.ProveedorId,
                Monto = dto.Monto,
                CuentaContableProveedor = dto.CuentaContableProveedor,
                CuentaContableBanco = dto.CuentaContableBanco
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al crear solicitud: {ex.Message}");
        }
    }

    /// <summary>
    /// Actualiza una solicitud de cheque
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<SolicitudChequeDto>> Update(int id, [FromBody] UpdateSolicitudChequeDto dto)
    {
        if (id != dto.Id)
            return BadRequest("El ID de la URL no coincide con el ID del DTO");

        try
        {
            var command = new UpdateSolicitudChequeCommand
            {
                Id = dto.Id,
                Monto = dto.Monto,
                CuentaContableProveedor = dto.CuentaContableProveedor,
                CuentaContableBanco = dto.CuentaContableBanco
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al actualizar solicitud: {ex.Message}");
        }
    }

    /// <summary>
    /// Genera un cheque para una solicitud
    /// </summary>
    [HttpPost("{id}/generar-cheque")]
    public async Task<ActionResult<SolicitudChequeDto>> GenerarCheque(int id, [FromBody] GenerarChequeDto dto)
    {
        if (id != dto.SolicitudId)
            return BadRequest("El ID de la URL no coincide con el ID del DTO");

        try
        {
            var command = new GenerarChequeCommand
            {
                SolicitudId = dto.SolicitudId,
                NumeroCheque = dto.NumeroCheque
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al generar cheque: {ex.Message}");
        }
    }

    /// <summary>
    /// Anula una solicitud de cheque (funciona para solicitudes pendientes y cheques generados)
    /// </summary>
    [HttpPost("{id}/anular")]
    public async Task<ActionResult<SolicitudChequeDto>> Anular(int id)
    {
        try
        {
            var command = new AnularSolicitudCommand { SolicitudId = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al anular solicitud: {ex.Message}");
        }
    }

    /// <summary>
    /// Anula un cheque generado (alias para mayor claridad semántica)
    /// </summary>
    [HttpPost("{id}/anular-cheque")]
    public async Task<ActionResult<SolicitudChequeDto>> AnularCheque(int id)
    {
        try
        {
            // Verificar primero que sea un cheque generado
            var queryById = new GetSolicitudByIdQuery { Id = id };
            var solicitud = await _mediator.Send(queryById);
            
            if (solicitud == null)
                return NotFound($"Solicitud con ID {id} no encontrada");

            if (solicitud.Estado != EstadoSolicitud.ChequeGenerado)
                return BadRequest("Solo se pueden anular cheques que han sido generados");

            var command = new AnularSolicitudCommand { SolicitudId = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al anular cheque: {ex.Message}");
        }
    }
} 