using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaCheques.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Endpoint para autenticación (generar token JWT)
    /// </summary>
    [HttpPost("login")]
    public Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        try
        {
            // TODO: Implementar autenticación real con base de datos
            if (IsValidUser(request.Username, request.Password))
            {
                var token = GenerateJwtToken(request.Username);
                return Task.FromResult<ActionResult<LoginResponse>>(Ok(new LoginResponse 
                { 
                    Token = token, 
                    ExpiresIn = 3600,
                    Username = request.Username,
                    Message = "Autenticación exitosa"
                }));
            }
            
            return Task.FromResult<ActionResult<LoginResponse>>(Unauthorized(new LoginResponse 
            { 
                Message = "Credenciales inválidas" 
            }));
        }
        catch (Exception ex)
        {
            return Task.FromResult<ActionResult<LoginResponse>>(BadRequest(new LoginResponse 
            { 
                Message = $"Error en autenticación: {ex.Message}" 
            }));
        }
    }

    /// <summary>
    /// Valida un token JWT
    /// </summary>
    [HttpPost("validate")]
    public Task<ActionResult<TokenValidationResponse>> ValidateToken([FromBody] TokenValidationRequest request)
    {
        try
        {
            var isValid = ValidateJwtToken(request.Token);
            return Task.FromResult<ActionResult<TokenValidationResponse>>(Ok(new TokenValidationResponse 
            { 
                IsValid = isValid,
                Message = isValid ? "Token válido" : "Token inválido"
            }));
        }
        catch (Exception ex)
        {
            return Task.FromResult<ActionResult<TokenValidationResponse>>(BadRequest(new TokenValidationResponse 
            { 
                IsValid = false,
                Message = $"Error al validar token: {ex.Message}" 
            }));
        }
    }

    /// <summary>
    /// Obtiene información del usuario actual
    /// </summary>
    [HttpGet("me")]
    public Task<ActionResult<UserInfoResponse>> GetCurrentUser()
    {
        try
        {
            // TODO: Implementar obtención de información real del usuario
            var username = User.Identity?.Name ?? "anonymous";
            return Task.FromResult<ActionResult<UserInfoResponse>>(Ok(new UserInfoResponse 
            { 
                Username = username,
                Roles = new[] { "user" },
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false
            }));
        }
        catch (Exception ex)
        {
            return Task.FromResult<ActionResult<UserInfoResponse>>(BadRequest(new UserInfoResponse 
            { 
                Message = $"Error al obtener información del usuario: {ex.Message}" 
            }));
        }
    }

    private bool IsValidUser(string username, string password)
    {
        // TODO: Implementar validación real contra base de datos
        return username == "admin" && password == "admin123";
    }

    private string GenerateJwtToken(string username)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "DefaultKey123456789012345678901234567890"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"] ?? "SistemaCheques",
            audience: _configuration["Jwt:Audience"] ?? "SistemaCheques",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private bool ValidateJwtToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "DefaultKey123456789012345678901234567890");

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"] ?? "SistemaCheques",
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"] ?? "SistemaCheques",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
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
    public string Username { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class TokenValidationRequest
{
    public string Token { get; set; } = string.Empty;
}

public class TokenValidationResponse
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class UserInfoResponse
{
    public string Username { get; set; } = string.Empty;
    public string[] Roles { get; set; } = Array.Empty<string>();
    public bool IsAuthenticated { get; set; }
    public string Message { get; set; } = string.Empty;
} 