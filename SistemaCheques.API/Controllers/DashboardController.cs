using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCheques.Infrastructure.Data;

namespace SistemaCheques.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene estadísticas generales del dashboard
    /// </summary>
    /// <returns>Estadísticas del sistema</returns>
    [HttpGet("stats")]
    public async Task<ActionResult<object>> GetStats()
    {
        try
        {
            var stats = new
            {
                TotalProveedores = await _context.Proveedores.CountAsync(p => p.Estado),
                TotalSolicitudesPendientes = await _context.SolicitudesCheques.CountAsync(s => s.Estado == Domain.Enums.EstadoSolicitud.Pendiente),
                TotalChequesGenerados = await _context.SolicitudesCheques.CountAsync(s => s.Estado == Domain.Enums.EstadoSolicitud.ChequeGenerado),
                TotalSolicitudesAnuladas = await _context.SolicitudesCheques.CountAsync(s => s.Estado == Domain.Enums.EstadoSolicitud.Anulada),
                MontoTotalPendiente = await _context.SolicitudesCheques
                    .Where(s => s.Estado == Domain.Enums.EstadoSolicitud.Pendiente)
                    .SumAsync(s => s.Monto),
                MontoTotalGenerado = await _context.SolicitudesCheques
                    .Where(s => s.Estado == Domain.Enums.EstadoSolicitud.ChequeGenerado)
                    .SumAsync(s => s.Monto),
                AsientosContablesPendientes = await _context.AsientosContables.CountAsync(a => !a.Enviado),
                AsientosContablesEnviados = await _context.AsientosContables.CountAsync(a => a.Enviado),
                ConceptosPagoActivos = await _context.ConceptosPago.CountAsync(c => c.Estado)
            };

            return Ok(stats);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener estadísticas", error = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene estadísticas de solicitudes por mes
    /// </summary>
    /// <returns>Estadísticas mensuales</returns>
    [HttpGet("monthly-stats")]
    public async Task<ActionResult<object>> GetMonthlyStats()
    {
        try
        {
            var currentYear = DateTime.UtcNow.Year;
            
            var monthlyStats = await _context.SolicitudesCheques
                .Where(s => s.FechaRegistro.Year == currentYear)
                .GroupBy(s => s.FechaRegistro.Month)
                .Select(g => new
                {
                    Mes = g.Key,
                    TotalSolicitudes = g.Count(),
                    MontoTotal = g.Sum(s => s.Monto),
                    Pendientes = g.Count(s => s.Estado == Domain.Enums.EstadoSolicitud.Pendiente),
                    Generados = g.Count(s => s.Estado == Domain.Enums.EstadoSolicitud.ChequeGenerado),
                    Anulados = g.Count(s => s.Estado == Domain.Enums.EstadoSolicitud.Anulada)
                })
                .OrderBy(x => x.Mes)
                .ToListAsync();

            return Ok(monthlyStats);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener estadísticas mensuales", error = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene los proveedores con más solicitudes
    /// </summary>
    /// <returns>Top proveedores</returns>
    [HttpGet("top-proveedores")]
    public async Task<ActionResult<object>> GetTopProveedores()
    {
        try
        {
            var topProveedores = await _context.Proveedores
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    p.CedulaRnc,
                    TipoPersona = p.TipoPersona == Domain.Enums.TipoPersona.Fisica ? "Física" : "Jurídica",
                    TotalSolicitudes = p.SolicitudesCheques.Count(),
                    MontoTotal = p.SolicitudesCheques.Sum(s => s.Monto),
                    SolicitudesPendientes = p.SolicitudesCheques.Count(s => s.Estado == Domain.Enums.EstadoSolicitud.Pendiente),
                    ChequesGenerados = p.SolicitudesCheques.Count(s => s.Estado == Domain.Enums.EstadoSolicitud.ChequeGenerado)
                })
                .Where(p => p.TotalSolicitudes > 0)
                .OrderByDescending(p => p.MontoTotal)
                .Take(10)
                .ToListAsync();

            return Ok(topProveedores);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener top proveedores", error = ex.Message });
        }
    }
}