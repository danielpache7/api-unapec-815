using Microsoft.EntityFrameworkCore;
using SistemaCheques.Domain.Entities;
using SistemaCheques.Domain.Enums;

namespace SistemaCheques.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ConceptoPago> ConceptosPago { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<SolicitudCheque> SolicitudesCheques { get; set; }
    public DbSet<AsientoContable> AsientosContables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de ConceptoPago
        modelBuilder.Entity<ConceptoPago>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Estado).IsRequired();
            entity.Property(e => e.FechaCreacion).IsRequired();
        });

        // Configuración de Proveedor
        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CedulaRnc).IsRequired().HasMaxLength(20);
            entity.Property(e => e.TipoPersona).HasConversion<int>();
            entity.Property(e => e.Balance).HasColumnType("decimal(18,2)");
            entity.Property(e => e.CuentaContableProveedor).HasMaxLength(50);
            entity.Property(e => e.Estado).IsRequired();
            entity.Property(e => e.FechaCreacion).IsRequired();
            
            entity.HasIndex(e => e.CedulaRnc).IsUnique();
        });

        // Configuración de SolicitudCheque
        modelBuilder.Entity<SolicitudCheque>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Monto).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(e => e.Estado).HasConversion<int>();
            entity.Property(e => e.CuentaContableProveedor).HasMaxLength(50);
            entity.Property(e => e.CuentaContableBanco).HasMaxLength(50);
            entity.Property(e => e.NumeroCheque).HasMaxLength(20);
            entity.Property(e => e.FechaRegistro).IsRequired();

            entity.HasOne(e => e.Proveedor)
                  .WithMany(p => p.SolicitudesCheques)
                  .HasForeignKey(e => e.ProveedorId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuración de AsientoContable
        modelBuilder.Entity<AsientoContable>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CuentaContable).IsRequired().HasMaxLength(50);
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.NumeroAsiento).HasMaxLength(20);
            entity.Property(e => e.Fecha).IsRequired();
            entity.Property(e => e.FechaCreacion).IsRequired();
        });

        // Datos iniciales
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConceptoPago>().HasData(
            new ConceptoPago
            {
                Id = 1,
                Descripcion = "Pago a Proveedores",
                Estado = true,
                FechaCreacion = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new ConceptoPago
            {
                Id = 2,
                Descripcion = "Pago de Servicios",
                Estado = true,
                FechaCreacion = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
} 