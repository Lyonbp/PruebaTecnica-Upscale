using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UpscalePruebaTecnica.Models;

public partial class UpscaleDbContext : DbContext
{
    public UpscaleDbContext()
    {
    }

    public UpscaleDbContext(DbContextOptions<UpscaleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TiposContratacion> TiposContratacions { get; set; }

    public virtual DbSet<TiposDocumento> TiposDocumentos { get; set; }

    public virtual DbSet<TiposTelefono> TiposTelefonos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<TiposContratacion>(entity =>
        {
            entity.HasKey(e => e.TipoContratacionId).HasName("PK__TiposCon__467E3BD555B2FF87");

            entity.ToTable("TiposContratacion");

            entity.Property(e => e.Abreviatura).HasMaxLength(10);
            entity.Property(e => e.Descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<TiposDocumento>(entity =>
        {
            entity.HasKey(e => e.TipoDocumentoId).HasName("PK__TiposDoc__A329EA87BF7C28CC");

            entity.ToTable("TiposDocumento");

            entity.Property(e => e.Abreviatura).HasMaxLength(10);
            entity.Property(e => e.Descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<TiposTelefono>(entity =>
        {
            entity.HasKey(e => e.TipoTelefonoId).HasName("PK__TiposTel__08B1AC81FFA4486E");

            entity.ToTable("TiposTelefono");

            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B88E138DCA");

            entity.HasIndex(e => e.NumeroDocumento, "UQ__Usuarios__A42025886AA5E1C1").IsUnique();

            entity.Property(e => e.Cargo).HasMaxLength(100);
            entity.Property(e => e.CorreoPrincipal).HasMaxLength(100);
            entity.Property(e => e.CorreoSecundario).HasMaxLength(100);
            entity.Property(e => e.FechaBloqueo).HasColumnType("datetime");
            entity.Property(e => e.Institucion).HasMaxLength(100);
            entity.Property(e => e.IntentosFallidos).HasDefaultValue(0);
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(50)
                .HasDefaultValue("Peruana");
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.NumeroDocumento).HasMaxLength(20);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PrimerApellido).HasMaxLength(100);
            entity.Property(e => e.SegundoApellido).HasMaxLength(100);
            entity.Property(e => e.Sexo).HasMaxLength(20);
            entity.Property(e => e.TelefonoMovil).HasMaxLength(20);
            entity.Property(e => e.TelefonoSecundario).HasMaxLength(20);

            entity.HasOne(d => d.TipoContratacion).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoContratacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__TipoCo__0B91BA14");

            entity.HasOne(d => d.TipoDocumento).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__TipoDo__0A9D95DB");

            entity.HasOne(d => d.TipoTelefonoSecundario).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoTelefonoSecundarioId)
                .HasConstraintName("FK__Usuarios__TipoTe__0E6E26BF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
