using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmpleadosABC.Models
{
    public partial class EmpleadosABCContext : DbContext
    {
        public EmpleadosABCContext()
        {
        }

        public EmpleadosABCContext(DbContextOptions<EmpleadosABCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Estatus> Estatuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.Property(e => e.EmpleadoId).HasColumnName("Empleado_Id");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Apellido_Materno");

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Apellido_Paterno");

                entity.Property(e => e.EstatusId).HasColumnName("Estatus_Id");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Nacimiento");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estatus)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.EstatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empleados_Estatus");
            });

            modelBuilder.Entity<Estatus>(entity =>
            {
                entity.ToTable("Estatus");

                entity.Property(e => e.EstatusId).HasColumnName("Estatus_Id");

                entity.Property(e => e.Descripción)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
