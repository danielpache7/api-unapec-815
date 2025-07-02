using MediatR;
using SistemaCheques.Application.DTOs;

namespace SistemaCheques.Application.Queries.ConceptoPago;

public class GetAllConceptosPagoQuery : IRequest<IEnumerable<ConceptoPagoDto>>
{
    public bool SoloActivos { get; set; } = false;
    
    public GetAllConceptosPagoQuery(bool soloActivos = false)
    {
        SoloActivos = soloActivos;
    }
} 