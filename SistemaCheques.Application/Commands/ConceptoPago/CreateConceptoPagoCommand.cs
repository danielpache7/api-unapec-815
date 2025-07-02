using MediatR;
using SistemaCheques.Application.DTOs;

namespace SistemaCheques.Application.Commands.ConceptoPago;

public class CreateConceptoPagoCommand : IRequest<ConceptoPagoDto>
{
    public string Descripcion { get; set; } = string.Empty;
    public bool Estado { get; set; } = true;
} 