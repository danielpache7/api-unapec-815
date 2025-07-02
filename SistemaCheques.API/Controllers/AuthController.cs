using Microsoft.AspNetCore.Mvc;

namespace SistemaCheques.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    /// <summary>
    /// Endpoint para autenticación (generar token JWT)
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        // TODO: Implementar autenticación real con AuthService
        if (request.Username == "admin" && request.Password == "admin123")
        {
            var token = "jwt-token-aqui"; // Generar con AuthService
            return Ok(new LoginResponse { Token = token, ExpiresIn = 3600 });
        }
        
        return Unauthorized("Credenciales inválidas");
    }
}

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
} 