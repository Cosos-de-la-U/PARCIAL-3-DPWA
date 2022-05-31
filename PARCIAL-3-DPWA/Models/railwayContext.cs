using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PARCIAL_3_DPWA.Models
{
    public partial class railwayContext : DbContext
    {
        public railwayContext()
        {
        }

        public railwayContext(DbContextOptions<railwayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Certificacion> Certificacions { get; set; } = null!;
        public virtual DbSet<Portafolio> Portafolios { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UsuarioCertificacion> UsuarioCertificacions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=containers-us-west-58.railway.app:7224;Database=railway;Username=postgres;Password=8xjhArQOUYh8lDI0Ru7O");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("timescaledb");

            modelBuilder.Entity<Certificacion>(entity =>
            {
                entity.ToTable("certificacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion).HasColumnName("descripcion");

                entity.Property(e => e.Institucion)
                    .HasMaxLength(255)
                    .HasColumnName("institucion");

                entity.Property(e => e.LinkAccesso)
                    .HasMaxLength(255)
                    .HasColumnName("link_accesso");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .HasColumnName("nombre");

                entity.Property(e => e.Objetivos).HasColumnName("objetivos");
            });

            modelBuilder.Entity<Portafolio>(entity =>
            {
                entity.ToTable("portafolio");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .HasColumnName("nombre");

                entity.Property(e => e.Responsabilidades).HasColumnName("responsabilidades");

                entity.Property(e => e.Resumen).HasColumnName("resumen");

                entity.Property(e => e.Rol)
                    .HasMaxLength(255)
                    .HasColumnName("rol");

                entity.Property(e => e.Tecnologias).HasColumnName("tecnologias");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Portafolios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usuario_portafolio");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(255)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Correo)
                    .HasMaxLength(255)
                    .HasColumnName("correo");

                entity.Property(e => e.FotoUrl)
                    .HasMaxLength(255)
                    .HasColumnName("foto_url");

                entity.Property(e => e.Introduccion).HasColumnName("introduccion");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(255)
                    .HasColumnName("nombres");
            });

            modelBuilder.Entity<UsuarioCertificacion>(entity =>
            {
                entity.ToTable("usuario_certificacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCertificacion).HasColumnName("id_certificacion");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdCertificacionNavigation)
                    .WithMany(p => p.UsuarioCertificacions)
                    .HasForeignKey(d => d.IdCertificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_certificacion");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioCertificacions)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usuario");
            });

            modelBuilder.HasSequence("chunk_constraint_name", "_timescaledb_catalog");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
