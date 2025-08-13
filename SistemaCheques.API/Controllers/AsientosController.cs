using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaCheques.Application.Commands.AsientoContable;
using SistemaCheques.Application.DTOs;
using SistemaCheques.Application.Queries.AsientoContable;

namespace SistemaCheques.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        try
        {
            var query = new GetAsientosContablesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener asientos: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtiene asientos contables por enviar
    /// </summary>
    [HttpGet("por-enviar")]
    public async Task<ActionResult<IEnumerable<AsientoContableDto>>> GetPorEnviar()
    {
        try
        {
            var query = new GetAsientosPorEnviarQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener asientos por enviar: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtiene asientos contables por fecha
    /// </summary>
    [HttpGet("por-fecha")]
    public async Task<ActionResult<IEnumerable<AsientoContableDto>>> GetByFecha([FromQuery] DateTime fecha)
    {
        try
        {
            var query = new GetAsientosPorFechaQuery { Fecha = fecha };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener asientos por fecha: {ex.Message}");
        }
    }

    /// <summary>
    /// Crea un nuevo asiento contable
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<AsientoContableDto>> Create([FromBody] CreateAsientoContableDto dto)
    {
        try
        {
            var command = new CreateAsientoContableCommand
            {
                Fecha = dto.Fecha,
                CuentaContable = dto.CuentaContable,
                MontoTotal = dto.MontoTotal,
                Descripcion = dto.Descripcion,
                NumeroAsiento = dto.NumeroAsiento
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAll), new { }, result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al crear asiento: {ex.Message}");
        }
    }

    /// <summary>
    /// Genera asiento contable sumando montos por cuenta desde solicitudes del mes
    /// </summary>
    [HttpPost("generar")]
    public async Task<ActionResult<AsientoContableDto>> GenerarAsiento([FromBody] GenerarAsientoContableDto dto)
    {
        try
        {
            var command = new GenerarAsientoContableCommand
            {
                Año = dto.Año,
                Mes = dto.Mes
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al generar asiento: {ex.Message}");
        }
    }

    /// <summary>
    /// Envía un asiento contable al sistema de contabilidad
    /// </summary>
    [HttpPost("{id}/enviar")]
    public async Task<ActionResult<AsientoContableDto>> EnviarAsiento(int id)
    {
        try
        {
            var command = new EnviarAsientoContableCommand { AsientoId = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al enviar asiento: {ex.Message}");
        }
    }

    /// <summary>
    /// Envía todos los asientos contables pendientes al sistema de contabilidad
    /// </summary>
    [HttpPost("enviar-todos")]
    public async Task<ActionResult<object>> EnviarTodosLosAsientos()
    {
        try
        {
            // Obtener todos los asientos pendientes
            var queryPendientes = new GetAsientosPorEnviarQuery();
            var asientosPendientes = await _mediator.Send(queryPendientes);

            if (!asientosPendientes.Any())
            {
                return Ok(new
                {
                    message = "No hay asientos pendientes por enviar",
                    asientosEnviados = 0,
                    asientosFallidos = 0,
                    detalles = new List<object>()
                });
            }

            var asientosEnviados = new List<AsientoContableDto>();
            var asientosFallidos = new List<object>();

            // Enviar cada asiento individualmente
            foreach (var asiento in asientosPendientes)
            {
                try
                {
                    var command = new EnviarAsientoContableCommand { AsientoId = asiento.Id };
                    var result = await _mediator.Send(command);
                    asientosEnviados.Add(result);
                }
                catch (Exception ex)
                {
                    asientosFallidos.Add(new
                    {
                        asientoId = asiento.Id,
                        numeroAsiento = asiento.NumeroAsiento,
                        error = ex.Message
                    });
                }
            }

            return Ok(new
            {
                message = $"Proceso completado. {asientosEnviados.Count} asientos enviados, {asientosFallidos.Count} fallidos",
                asientosEnviados = asientosEnviados.Count,
                asientosFallidos = asientosFallidos.Count,
                detalles = new
                {
                    enviados = asientosEnviados.Select(a => new
                    {
                        id = a.Id,
                        numeroAsiento = a.NumeroAsiento,
                        cuentaContable = a.CuentaContable,
                        montoTotal = a.MontoTotal,
                        fechaEnvio = a.FechaEnvio
                    }),
                    fallidos = asientosFallidos
                }
            });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al enviar asientos: {ex.Message}");
        }
    }
} 