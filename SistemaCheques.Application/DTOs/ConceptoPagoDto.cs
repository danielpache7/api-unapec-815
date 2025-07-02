namespace SistemaCheques.Application.DTOs;

public class ConceptoPagoDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
}

public class CreateConceptoPagoDto
{
    public string Descripcion { get; set; } = string.Empty;
    public bool Estado { get; set; } = true;
}

public class UpdateConceptoPagoDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public bool Estado { get; set; }
} 