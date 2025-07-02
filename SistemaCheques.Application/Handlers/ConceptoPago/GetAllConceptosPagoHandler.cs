using MediatR;
using SistemaCheques.Application.DTOs;
using SistemaCheques.Application.Mappings;
using SistemaCheques.Application.Queries.ConceptoPago;
using SistemaCheques.Domain.Interfaces;

namespace SistemaCheques.Application.Handlers.ConceptoPago;

public class GetAllConceptosPagoHandler : IRequestHandler<GetAllConceptosPagoQuery, IEnumerable<ConceptoPagoDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllConceptosPagoHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ConceptoPagoDto>> Handle(GetAllConceptosPagoQuery request, CancellationToken cancellationToken)
    {
        var entities = request.SoloActivos 
            ? await _unitOfWork.ConceptosPago.GetActivosAsync()
            : await _unitOfWork.ConceptosPago.GetAllAsync();

        return entities.Select(e => e.ToDto());
    }
} 