using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SenaiSystem.Models;

namespace SenaiSystem.Context;

public partial class SenaiSystemContext : DbContext
{
    public SenaiSystemContext()
    {
    }

    private IConfiguration _configuration;
    public SenaiSystemContext(DbContextOptions<SenaiSystemContext> options, IConfiguration confing)
        : base(options)
    { 
        _configuration = confing;   
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Lembrete> Lembretes { get; set; }

    public virtual DbSet<NotaCategoria> NotaCategoria { get; set; }

    public virtual DbSet<Nota> Nota { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
        var con = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(con);

        }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A1085F3B329");

            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Lembrete>(entity =>
        {
            entity.HasKey(e => e.IdLembrete).HasName("PK__Lembrete__07C2D3EC85BBA073");

            entity.ToTable("Lembrete");

            entity.Property(e => e.DataHora).HasColumnType("datetime");

            entity.HasOne(d => d.IdNotaNavigation).WithMany(p => p.Lembretes)
                .HasForeignKey(d => d.IdNota)
                .HasConstraintName("FK__Lembrete__IdNota__3B40CD36");
        });

        modelBuilder.Entity<NotaCategoria>(entity =>
        {
            entity.HasKey(e => e.IdNotaCategoria).HasName("PK__NotaCate__00287517816CB1F4");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.NotaCategoria)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__NotaCateg__IdCat__40058253");

            entity.HasOne(d => d.IdNotaNavigation).WithMany(p => p.NotaCategoria)
                .HasForeignKey(d => d.IdNota)
                .HasConstraintName("FK__NotaCateg__IdNot__40F9A68C");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.IdNota).HasName("PK__Nota__4B2ACFF28580AFD6");

            entity.Property(e => e.Conteudo).HasColumnType("text");
            entity.Property(e => e.DataCriacao).HasColumnType("datetime");
            entity.Property(e => e.DataModificacao).HasColumnType("datetime");
            entity.Property(e => e.Imagem)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Nota)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Nota__IdUsuario__3864608B");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97E254AFC6");

            entity.ToTable("Usuario");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
