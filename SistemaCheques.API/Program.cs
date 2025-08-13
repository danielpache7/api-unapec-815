using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SistemaCheques.Application.Handlers.ConceptoPago;
using SistemaCheques.Domain.Interfaces;
using SistemaCheques.Infrastructure.Data;
using SistemaCheques.Infrastructure.Repositories;
using System.Reflection;
using System.Text;

// Configuración global para PostgreSQL y DateTime
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Configuración de Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de repositorios y Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IConceptoPagoRepository, ConceptoPagoRepository>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<ISolicitudChequeRepository, SolicitudChequeRepository>();
builder.Services.AddScoped<IAsientoContableRepository, AsientoContableRepository>();

// Configuración de MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateConceptoPagoHandler).Assembly);
});

// Configuración de JWT - DESHABILITADA
// var jwtKey = builder.Configuration["Jwt:Key"];
// var jwtIssuer = builder.Configuration["Jwt:Issuer"];
// var jwtAudience = builder.Configuration["Jwt:Audience"];

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = jwtIssuer,
//             ValidAudience = jwtAudience,
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? ""))
//         };
//     });

// builder.Services.AddAuthorization();

// Configuración de controladores
builder.Services.AddControllers();

// Configuración de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sistema de Cheques API",
        Version = "v1",
        Description = "API RESTful para el Sistema de Cheques con arquitectura limpia",
        Contact = new OpenApiContact
        {
            Name = "Equipo de Desarrollo",
            Email = "desarrollo@empresa.com"
        }
    });

    // Configuración de seguridad JWT en Swagger - DESHABILITADA
    // c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    // {
    //     Description = "JWT Authorization header usando el esquema Bearer. Ejemplo: \"Authorization: Bearer {token}\"",
    //     Name = "Authorization",
    //     In = ParameterLocation.Header,
    //     Type = SecuritySchemeType.ApiKey,
    //     Scheme = "Bearer"
    // });

    // c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    // {
    //     {
    //         new OpenApiSecurityScheme
    //         {
    //             Reference = new OpenApiReference
    //             {
    //                 Type = ReferenceType.SecurityScheme,
    //                 Id = "Bearer"
    //             },
    //             Scheme = "oauth2",
    //             Name = "Bearer",
    //             In = ParameterLocation.Header,
    //         },
    //         new List<string>()
    //     }
    // });

    // Incluir comentarios XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Configuración de CORS - COMPLETAMENTE ABIERTO
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()   // Permite cualquier origen
              .AllowAnyHeader()   // Permite cualquier header
              .AllowAnyMethod();  // Permite cualquier método
    });
});

// Configuración de logging
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
});

// Configuración específica para logging de CORS
builder.Services.AddLogging(logging =>
{
    logging.AddFilter("Microsoft.AspNetCore.Cors", LogLevel.Debug);
});

var app = builder.Build();

// Pipeline de configuración HTTP
// Swagger habilitado en todos los ambientes (Development y Production)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema de Cheques API v1");
    c.RoutePrefix = string.Empty; // Swagger UI en la raíz
});

app.UseHttpsRedirection();

// Middleware personalizado para debug de CORS
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");
    logger.LogInformation($"Origin: {context.Request.Headers["Origin"]}");
    logger.LogInformation($"User-Agent: {context.Request.Headers["User-Agent"]}");
    
    await next();
    
    logger.LogInformation($"Response Status: {context.Response.StatusCode}");
});

// CORS habilitado para todos los orígenes
app.UseCors("AllowAllOrigins");

// Autenticación y autorización deshabilitadas
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

// Migración automática de la base de datos en desarrollo
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error durante la migración de la base de datos");
    }
}

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
