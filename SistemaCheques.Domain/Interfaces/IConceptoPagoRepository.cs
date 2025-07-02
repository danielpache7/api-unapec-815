using SistemaCheques.Domain.Entities;

namespace SistemaCheques.Domain.Interfaces;

public interface IConceptoPagoRepository : IRepository<ConceptoPago>
{
    Task<IEnumerable<ConceptoPago>> GetActivosAsync();
} 