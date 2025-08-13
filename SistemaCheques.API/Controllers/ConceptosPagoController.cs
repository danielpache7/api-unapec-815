using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCheques.Application.Commands.ConceptoPago;
using SistemaCheques.Application.DTOs;
using SistemaCheques.Application.Queries.ConceptoPago;

namespace SistemaCheques.API.Controllers;

[ApiController]
[Route("api/ConceptosPago")] // Ruta compatible con el frontend
// [Authorize] // Temporalmente deshabilitado para testing
public class ConceptosPagoController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConceptosPagoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene todos los conceptos de pago
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConceptoPagoDto>>> GetAll([FromQuery] bool soloActivos = false)
    {
        var query = new GetAllConceptosPagoQuery(soloActivos);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Obtiene un concepto de pago por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ConceptoPagoDto>> GetById(int id)
    {
        var query = new GetConceptoPagoByIdQuery(id);
        var result = await _mediator.Send(query);
        
        if (result == null)
            return NotFound($"Concepto de pago con ID {id} no encontrado");
            
        return Ok(result);
    }

    /// <summary>
    /// Crea un nuevo concepto de pago
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ConceptoPagoDto>> Create([FromBody] CreateConceptoPagoCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Actualiza un concepto de pago existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ConceptoPagoDto>> Update(int id, [FromBody] UpdateConceptoPagoCommand command)
    {
        if (id != command.Id)
            return BadRequest("El ID de la URL no coincide con el ID del comando");

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Elimina un concepto de pago
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteConceptoPagoCommand(id);
        var result = await _mediator.Send(command);
        
        if (!result)
            return NotFound($"Concepto de pago con ID {id} no encontrado");
            
        return NoContent();
    }

    /// <summary>
    /// Endpoint de prueba para verificar CORS
    /// </summary>
    [HttpGet("test-cors")]
    public ActionResult TestCors()
    {
        return Ok(new { 
            message = "CORS está funcionando correctamente",
            timestamp = DateTime.UtcNow,
            headers = Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())
        });
    }
} 