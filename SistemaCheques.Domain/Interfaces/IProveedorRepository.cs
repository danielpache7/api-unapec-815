using SistemaCheques.Domain.Entities;
using SistemaCheques.Domain.Enums;

namespace SistemaCheques.Domain.Interfaces;

public interface IProveedorRepository : IRepository<Proveedor>
{
    Task<IEnumerable<Proveedor>> GetActivosAsync();
    Task<Proveedor?> GetByCedulaRncAsync(string cedulaRnc);
    Task<IEnumerable<Proveedor>> GetByTipoPersonaAsync(TipoPersona tipoPersona);
} 