﻿using System;
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

    public virtual DbSet<AuditoriaGeral> AuditoriaGerals { get; set; }

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
        modelBuilder.Entity<AuditoriaGeral>(entity =>
        {
            entity.HasKey(e => e.IdAuditoria).HasName("PK__Auditori__7FD13FA0E0F53C00");

            entity.ToTable("AuditoriaGeral");

            entity.Property(e => e.DataAcao).HasColumnType("datetime");
            entity.Property(e => e.NomeTabela)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TipoAcao)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A108CC4DC94");

            entity.HasIndex(e => e.Nome, "idx_NomeCategoria");

            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Categoria__IdUsu__14270015");
        });

        modelBuilder.Entity<Lembrete>(entity =>
        {
            entity.HasKey(e => e.IdLembrete).HasName("PK__Lembrete__07C2D3EC0BEB3694");

            entity.ToTable("Lembrete");

            entity.Property(e => e.DataHora).HasColumnType("datetime");

            entity.HasOne(d => d.IdNotaNavigation).WithMany(p => p.Lembretes)
                .HasForeignKey(d => d.IdNota)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Lembrete__IdNota");
        });

        modelBuilder.Entity<NotaCategoria>(entity =>
        {
            entity.HasKey(e => e.IdNotaCategoria).HasName("PK__NotaCate__00287517C712550D");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.NotaCategoria)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__NotaCateg__IdCat__6FE99F9F");

            entity.HasOne(d => d.IdNotaNavigation).WithMany(p => p.NotaCategoria)
                .HasForeignKey(d => d.IdNota)
                .HasConstraintName("FK__NotaCateg__IdNot__70DDC3D8");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.IdNota).HasName("PK__Nota__4B2ACFF272F8CB29");

            entity.HasIndex(e => e.Imagem, "idx_Imagem");

            entity.HasIndex(e => e.Titulo, "idx_Titulo");

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
                .HasConstraintName("FK__Nota__IdUsuario__5EBF139D");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9739454805");

            entity.ToTable("Usuario", tb =>
                {
                    tb.HasTrigger("trg_audit_categoria");
                    tb.HasTrigger("trg_audit_lembrete");
                    tb.HasTrigger("trg_audit_nota");
                    tb.HasTrigger("trg_audit_notacategoria");
                    tb.HasTrigger("trg_audit_usuario");
                });

            entity.HasIndex(e => e.Email, "idx_Email");

            entity.HasIndex(e => e.Nome, "idx_Nome");

            entity.HasIndex(e => e.Senha, "idx_Senha");

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
