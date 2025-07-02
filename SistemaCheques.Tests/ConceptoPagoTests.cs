using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SistemaCheques.Application.DTOs;
using SistemaCheques.Application.Handlers.ConceptoPago;
using SistemaCheques.Application.Commands.ConceptoPago;
using SistemaCheques.Application.Mappings;
using SistemaCheques.Domain.Entities;
using SistemaCheques.Infrastructure.Data;
using SistemaCheques.Infrastructure.Repositories;
using Xunit;

namespace SistemaCheques.Tests;

public class ConceptoPagoTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly UnitOfWork _unitOfWork;

    public ConceptoPagoTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _unitOfWork = new UnitOfWork(_context);
    }

    [Fact]
    public async Task CreateConceptoPago_ShouldReturnValidDto()
    {
        // Arrange
        var handler = new CreateConceptoPagoHandler(_unitOfWork);
        var command = new CreateConceptoPagoCommand
        {
            Descripcion = "Concepto de prueba",
            Estado = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Descripcion.Should().Be("Concepto de prueba");
        result.Estado.Should().BeTrue();
        result.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public void ConceptoPago_ToDto_ShouldMapCorrectly()
    {
        // Arrange
        var entity = new ConceptoPago
        {
            Id = 1,
            Descripcion = "Test Concepto",
            Estado = true,
            FechaCreacion = DateTime.UtcNow
        };

        // Act
        var dto = entity.ToDto();

        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(1);
        dto.Descripcion.Should().Be("Test Concepto");
        dto.Estado.Should().BeTrue();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
} 