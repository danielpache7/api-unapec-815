namespace SistemaCheques.Application.DTOs;

public class AsientoContableDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string CuentaContable { get; set; } = string.Empty;
    public decimal MontoTotal { get; set; }
    public string? Descripcion { get; set; }
    public string? NumeroAsiento { get; set; }
    public bool Enviado { get; set; }
    public DateTime? FechaEnvio { get; set; }
    public DateTime FechaCreacion { get; set; }
}

public class CreateAsientoContableDto
{
    public DateTime Fecha { get; set; }
    public string CuentaContable { get; set; } = string.Empty;
    public decimal MontoTotal { get; set; }
    public string? Descripcion { get; set; }
    public string? NumeroAsiento { get; set; }
}

public class GenerarAsientoContableDto
{
    public int Año { get; set; }
    public int Mes { get; set; }
} 