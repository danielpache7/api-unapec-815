using Microsoft.EntityFrameworkCore;
using SistemaCheques.Domain.Entities;
using SistemaCheques.Domain.Enums;
using SistemaCheques.Domain.Interfaces;
using SistemaCheques.Infrastructure.Data;

namespace SistemaCheques.Infrastructure.Repositories;

public class SolicitudChequeRepository : Repository<SolicitudCheque>, ISolicitudChequeRepository
{
    public SolicitudChequeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<SolicitudCheque>> GetByProveedorAsync(int proveedorId)
    {
        return await _dbSet
            .Include(s => s.Proveedor)
            .Where(s => s.ProveedorId == proveedorId)
            .OrderByDescending(s => s.FechaRegistro)
            .ToListAsync();
    }

    public async Task<IEnumerable<SolicitudCheque>> GetByEstadoAsync(EstadoSolicitud estado)
    {
        return await _dbSet
            .Include(s => s.Proveedor)
            .Where(s => s.Estado == estado)
            .OrderByDescending(s => s.FechaRegistro)
            .ToListAsync();
    }

    public async Task<IEnumerable<SolicitudCheque>> GetByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        return await _dbSet
            .Include(s => s.Proveedor)
            .Where(s => s.FechaRegistro >= fechaInicio && s.FechaRegistro <= fechaFin)
            .OrderByDescending(s => s.FechaRegistro)
            .ToListAsync();
    }

    public async Task<IEnumerable<SolicitudCheque>> GetPendientesAsync()
    {
        return await _dbSet
            .Include(s => s.Proveedor)
            .Where(s => s.Estado == EstadoSolicitud.Pendiente)
            .OrderByDescending(s => s.FechaRegistro)
            .ToListAsync();
    }

    public async Task<IEnumerable<SolicitudCheque>> GetSolicitudesDelMesAsync(int año, int mes)
    {
        return await _dbSet
            .Include(s => s.Proveedor)
            .Where(s => s.FechaRegistro.Year == año && s.FechaRegistro.Month == mes)
            .OrderByDescending(s => s.FechaRegistro)
            .ToListAsync();
    }
} 