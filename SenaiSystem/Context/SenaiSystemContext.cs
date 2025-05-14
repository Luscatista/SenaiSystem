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

    public SenaiSystemContext(DbContextOptions<SenaiSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Lembrete> Lembretes { get; set; }

    public virtual DbSet<NotaCategorium> NotaCategoria { get; set; }

    public virtual DbSet<Nota> Nota { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-8ED5M2P\\SQLEXPRESS;Initial Catalog=SenaiSystem;User id=sa;Password=Senai@134;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A104EB71C00");

            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNotaNavigation).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.IdNota)
                .HasConstraintName("FK__Categoria__IdNot__30C33EC3");
        });

        modelBuilder.Entity<Lembrete>(entity =>
        {
            entity.HasKey(e => e.IdLembrete).HasName("PK__Lembrete__07C2D3EC04420E55");

            entity.ToTable("Lembrete");

            entity.Property(e => e.DataHora).HasColumnType("datetime");

            entity.HasOne(d => d.IdNotaNavigation).WithMany(p => p.Lembretes)
                .HasForeignKey(d => d.IdNota)
                .HasConstraintName("FK__Lembrete__IdNota__2DE6D218");
        });

        modelBuilder.Entity<NotaCategorium>(entity =>
        {
            entity.HasKey(e => e.IdNotaCategoria).HasName("PK__NotaCate__0028751737CAB61F");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.NotaCategoria)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__NotaCateg__IdCat__339FAB6E");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.IdNota).HasName("PK__Nota__4B2ACFF22F9655D2");

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
                .HasConstraintName("FK__Nota__IdUsuario__22751F6C");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9737BEEADE");

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
