using SistemaCheques.Domain.Entities;
using SistemaCheques.Domain.Enums;

namespace SistemaCheques.Domain.Interfaces;

public interface ISolicitudChequeRepository : IRepository<SolicitudCheque>
{
    Task<IEnumerable<SolicitudCheque>> GetByProveedorAsync(int proveedorId);
    Task<IEnumerable<SolicitudCheque>> GetByEstadoAsync(EstadoSolicitud estado);
    Task<IEnumerable<SolicitudCheque>> GetByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin);
    Task<IEnumerable<SolicitudCheque>> GetPendientesAsync();
    Task<IEnumerable<SolicitudCheque>> GetSolicitudesDelMesAsync(int año, int mes);
} 