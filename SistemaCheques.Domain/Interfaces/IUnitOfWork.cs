namespace SistemaCheques.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IConceptoPagoRepository ConceptosPago { get; }
    IProveedorRepository Proveedores { get; }
    ISolicitudChequeRepository SolicitudesCheques { get; }
    IAsientoContableRepository AsientosContables { get; }
    
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
} 