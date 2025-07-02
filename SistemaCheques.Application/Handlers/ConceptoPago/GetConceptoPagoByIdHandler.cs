using MediatR;
using SistemaCheques.Application.DTOs;
using SistemaCheques.Application.Mappings;
using SistemaCheques.Application.Queries.ConceptoPago;
using SistemaCheques.Domain.Interfaces;

namespace SistemaCheques.Application.Handlers.ConceptoPago;

public class GetConceptoPagoByIdHandler : IRequestHandler<GetConceptoPagoByIdQuery, ConceptoPagoDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetConceptoPagoByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ConceptoPagoDto?> Handle(GetConceptoPagoByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.ConceptosPago.GetByIdAsync(request.Id);
        return entity?.ToDto();
    }
} 