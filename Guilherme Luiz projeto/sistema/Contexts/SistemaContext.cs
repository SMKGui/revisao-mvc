﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using sistema.Models;

namespace sistema.Contexts;

public partial class SistemaContext : DbContext
{
    public SistemaContext()
    {
    }

    public SistemaContext(DbContextOptions<SistemaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Atividade> Atividades { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Turma> Turmas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-MHF127S; initial catalog=dev_db_tarde_guiluiz; User Id =sa; Pwd= Senai@134;  TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atividade>(entity =>
        {
            entity.HasKey(e => e.AtividadeId).HasName("PK__Atividad__742A5DF497034E95");

            entity.ToTable("Atividade");

            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Turma).WithMany(p => p.Atividades)
                .HasForeignKey(d => d.TurmaId)
                .HasConstraintName("FK__Atividade__Turma__3D5E1FD2");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.ProfessorId).HasName("PK__Professo__90035949B243E6C0");

            entity.ToTable("Professor");

            entity.HasIndex(e => e.Email, "UQ__Professo__A9D1053470ED67F4").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Turma>(entity =>
        {
            entity.HasKey(e => e.TurmaId).HasName("PK__Turma__BABB9304CE4FD700");

            entity.ToTable("Turma");

            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Professor).WithMany(p => p.Turmas)
                .HasForeignKey(d => d.ProfessorId)
                .HasConstraintName("FK__Turma__Professor__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
