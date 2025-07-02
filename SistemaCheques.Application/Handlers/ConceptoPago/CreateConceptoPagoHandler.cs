using MediatR;
using SistemaCheques.Application.Commands.ConceptoPago;
using SistemaCheques.Application.DTOs;
using SistemaCheques.Application.Mappings;
using SistemaCheques.Domain.Interfaces;

namespace SistemaCheques.Application.Handlers.ConceptoPago;

public class CreateConceptoPagoHandler : IRequestHandler<CreateConceptoPagoCommand, ConceptoPagoDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateConceptoPagoHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ConceptoPagoDto> Handle(CreateConceptoPagoCommand request, CancellationToken cancellationToken)
    {
        var createDto = new CreateConceptoPagoDto
        {
            Descripcion = request.Descripcion,
            Estado = request.Estado
        };

        var entity = createDto.ToEntity();
        await _unitOfWork.ConceptosPago.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return entity.ToDto();
    }
} 