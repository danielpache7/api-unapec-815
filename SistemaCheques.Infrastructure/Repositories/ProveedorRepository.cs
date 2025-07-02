using Microsoft.EntityFrameworkCore;
using SistemaCheques.Domain.Entities;
using SistemaCheques.Domain.Enums;
using SistemaCheques.Domain.Interfaces;
using SistemaCheques.Infrastructure.Data;

namespace SistemaCheques.Infrastructure.Repositories;

public class ProveedorRepository : Repository<Proveedor>, IProveedorRepository
{
    public ProveedorRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Proveedor>> GetActivosAsync()
    {
        return await _dbSet.Where(p => p.Estado).ToListAsync();
    }

    public async Task<Proveedor?> GetByCedulaRncAsync(string cedulaRnc)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.CedulaRnc == cedulaRnc);
    }

    public async Task<IEnumerable<Proveedor>> GetByTipoPersonaAsync(TipoPersona tipoPersona)
    {
        return await _dbSet.Where(p => p.TipoPersona == tipoPersona && p.Estado).ToListAsync();
    }
} 