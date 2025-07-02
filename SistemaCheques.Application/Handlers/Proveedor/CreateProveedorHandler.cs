using MediatR;
using SistemaCheques.Application.Commands.Proveedor;
using SistemaCheques.Application.DTOs;
using SistemaCheques.Application.Mappings;
using SistemaCheques.Domain.Interfaces;

namespace SistemaCheques.Application.Handlers.Proveedor;

public class CreateProveedorHandler : IRequestHandler<CreateProveedorCommand, ProveedorDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProveedorHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProveedorDto> Handle(CreateProveedorCommand request, CancellationToken cancellationToken)
    {
        // Validar que no exista ya un proveedor con esa cédula/RNC
        var existingProveedor = await _unitOfWork.Proveedores.GetByCedulaRncAsync(request.CedulaRnc);
        if (existingProveedor != null)
        {
            throw new InvalidOperationException($"Ya existe un proveedor con cédula/RNC: {request.CedulaRnc}");
        }

        var createDto = new CreateProveedorDto
        {
            Nombre = request.Nombre,
            TipoPersona = request.TipoPersona,
            CedulaRnc = request.CedulaRnc,
            Balance = request.Balance,
            CuentaContableProveedor = request.CuentaContableProveedor,
            Estado = request.Estado
        };

        var entity = createDto.ToEntity();
        await _unitOfWork.Proveedores.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return entity.ToDto();
    }
} 