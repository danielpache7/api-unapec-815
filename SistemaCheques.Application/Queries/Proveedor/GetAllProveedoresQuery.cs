using MediatR;
using SistemaCheques.Application.DTOs;

namespace SistemaCheques.Application.Queries.Proveedor;

public class GetAllProveedoresQuery : IRequest<IEnumerable<ProveedorDto>>
{
    public bool SoloActivos { get; set; } = false;
    
    public GetAllProveedoresQuery(bool soloActivos = false)
    {
        SoloActivos = soloActivos;
    }
} 