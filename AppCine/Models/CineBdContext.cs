using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppCine.Models;

public partial class CineBdContext : DbContext
{
    public CineBdContext()
    {
    }

    public CineBdContext(DbContextOptions<CineBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asiento> Asientos { get; set; }

    public virtual DbSet<Boleto> Boletos { get; set; }

    public virtual DbSet<Funcione> Funciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-DHCV675;Database=CineBD;Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asiento>(entity =>
        {
            entity.HasKey(e => e.AsientoId).HasName("PK__Asientos__04904D3007C4CE9F");

            entity.Property(e => e.AsientoId).HasColumnName("AsientoID");
            entity.Property(e => e.Fila)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FuncionId).HasColumnName("FuncionID");

            entity.HasOne(d => d.Funcion).WithMany(p => p.Asientos)
                .HasForeignKey(d => d.FuncionId)
                .HasConstraintName("FK__Asientos__Funcio__398D8EEE");
        });

        modelBuilder.Entity<Boleto>(entity =>
        {
            entity.HasKey(e => e.BoletoId).HasName("PK__Boletos__C5AF443469806661");

            entity.Property(e => e.BoletoId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("BoletoID");
            entity.Property(e => e.AsientoId).HasColumnName("AsientoID");
            entity.Property(e => e.FuncionId).HasColumnName("FuncionID");

            entity.HasOne(d => d.Asiento).WithMany(p => p.Boletos)
                .HasForeignKey(d => d.AsientoId)
                .HasConstraintName("FK__Boletos__Asiento__3D5E1FD2");

            entity.HasOne(d => d.Funcion).WithMany(p => p.Boletos)
                .HasForeignKey(d => d.FuncionId)
                .HasConstraintName("FK__Boletos__Funcion__3C69FB99");
        });

        modelBuilder.Entity<Funcione>(entity =>
        {
            entity.HasKey(e => e.FuncionId).HasName("PK__Funcione__F22456E492F945FA");

            entity.Property(e => e.FuncionId).HasColumnName("FuncionID");
            entity.Property(e => e.FechaHora).HasColumnType("datetime");
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
