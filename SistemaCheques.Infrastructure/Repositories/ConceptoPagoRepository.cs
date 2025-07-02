using Microsoft.EntityFrameworkCore;
using SistemaCheques.Domain.Entities;
using SistemaCheques.Domain.Interfaces;
using SistemaCheques.Infrastructure.Data;

namespace SistemaCheques.Infrastructure.Repositories;

public class ConceptoPagoRepository : Repository<ConceptoPago>, IConceptoPagoRepository
{
    public ConceptoPagoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ConceptoPago>> GetActivosAsync()
    {
        return await _dbSet.Where(c => c.Estado).ToListAsync();
    }
} 