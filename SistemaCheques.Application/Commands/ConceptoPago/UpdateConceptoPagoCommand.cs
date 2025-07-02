using MediatR;
using SistemaCheques.Application.DTOs;

namespace SistemaCheques.Application.Commands.ConceptoPago;

public class UpdateConceptoPagoCommand : IRequest<ConceptoPagoDto>
{
    public int Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public bool Estado { get; set; }
} 