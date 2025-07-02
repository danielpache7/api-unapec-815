using MediatR;
using SistemaCheques.Application.DTOs;

namespace SistemaCheques.Application.Queries.ConceptoPago;

public class GetConceptoPagoByIdQuery : IRequest<ConceptoPagoDto?>
{
    public int Id { get; set; }
    
    public GetConceptoPagoByIdQuery(int id)
    {
        Id = id;
    }
} 