using Microsoft.EntityFrameworkCore.Storage;
using SistemaCheques.Domain.Interfaces;
using SistemaCheques.Infrastructure.Data;

namespace SistemaCheques.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    private IConceptoPagoRepository? _conceptosPago;
    private IProveedorRepository? _proveedores;
    private ISolicitudChequeRepository? _solicitudesCheques;
    private IAsientoContableRepository? _asientosContables;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IConceptoPagoRepository ConceptosPago
    {
        get { return _conceptosPago ??= new ConceptoPagoRepository(_context); }
    }

    public IProveedorRepository Proveedores
    {
        get { return _proveedores ??= new ProveedorRepository(_context); }
    }

    public ISolicitudChequeRepository SolicitudesCheques
    {
        get { return _solicitudesCheques ??= new SolicitudChequeRepository(_context); }
    }

    public IAsientoContableRepository AsientosContables
    {
        get { return _asientosContables ??= new AsientoContableRepository(_context); }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
} 