using MediatR;

namespace SistemaCheques.Application.Commands.ConceptoPago;

public class DeleteConceptoPagoCommand : IRequest<bool>
{
    public int Id { get; set; }
    
    public DeleteConceptoPagoCommand(int id)
    {
        Id = id;
    }
} 