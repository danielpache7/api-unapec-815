using SistemaCheques.Domain.Entities;

namespace SistemaCheques.Domain.Interfaces;

public interface IAsientoContableRepository : IRepository<AsientoContable>
{
    Task<IEnumerable<AsientoContable>> GetPorEnviarAsync();
    Task<IEnumerable<AsientoContable>> GetByFechaAsync(DateTime fecha);
    Task<IEnumerable<AsientoContable>> GetByCuentaContableAsync(string cuentaContable);
} 