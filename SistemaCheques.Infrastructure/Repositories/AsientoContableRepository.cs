using Microsoft.EntityFrameworkCore;
using SistemaCheques.Domain.Entities;
using SistemaCheques.Domain.Interfaces;
using SistemaCheques.Infrastructure.Data;

namespace SistemaCheques.Infrastructure.Repositories;

public class AsientoContableRepository : Repository<AsientoContable>, IAsientoContableRepository
{
    public AsientoContableRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<AsientoContable>> GetPorEnviarAsync()
    {
        return await _dbSet
            .Where(a => !a.Enviado)
            .OrderBy(a => a.Fecha)
            .ToListAsync();
    }

    public async Task<IEnumerable<AsientoContable>> GetByFechaAsync(DateTime fecha)
    {
        return await _dbSet
            .Where(a => a.Fecha.Date == fecha.Date)
            .OrderBy(a => a.FechaCreacion)
            .ToListAsync();
    }

    public async Task<IEnumerable<AsientoContable>> GetByCuentaContableAsync(string cuentaContable)
    {
        return await _dbSet
            .Where(a => a.CuentaContable == cuentaContable)
            .OrderByDescending(a => a.Fecha)
            .ToListAsync();
    }
} 