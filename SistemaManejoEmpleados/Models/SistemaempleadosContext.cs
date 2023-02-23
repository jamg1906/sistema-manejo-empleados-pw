using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaManejoEmpleados.Models;

public partial class SistemaempleadosContext : DbContext
{
    public SistemaempleadosContext()
    {
    }

    public SistemaempleadosContext(DbContextOptions<SistemaempleadosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Puesto> Puestos { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)

        {

            IConfigurationRoot configuration = new ConfigurationBuilder()

            .SetBasePath(Directory.GetCurrentDirectory())

                        .AddJsonFile("appsettings.json")

                        .Build();


            var connectionString = configuration.GetConnectionString("DBEmpleados");

            optionsBuilder.UseMySQL(connectionString);

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PRIMARY");

            entity.ToTable("departamento");

            entity.HasIndex(e => e.IdDireccion, "idDireccion_Direccion_Departamento_idx");

            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.DescripcionDepartamento)
                .HasMaxLength(100)
                .HasColumnName("descripcionDepartamento");
            entity.Property(e => e.IdDireccion).HasColumnName("idDireccion");
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(100)
                .HasColumnName("nombreDepartamento");

            entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.IdDireccion)
                .HasConstraintName("idDireccion_Direccion_Departamento");
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PRIMARY");

            entity.ToTable("direccion");

            entity.HasIndex(e => e.Dpidirector, "DPI_idx");

            entity.Property(e => e.IdDireccion).HasColumnName("idDireccion");
            entity.Property(e => e.DescripcionDireccion)
                .HasMaxLength(100)
                .HasColumnName("descripcionDireccion");
            entity.Property(e => e.Dpidirector).HasColumnName("DPIDirector");
            entity.Property(e => e.NombreDireccion)
                .HasMaxLength(100)
                .HasColumnName("nombreDireccion");

            entity.HasOne(d => d.DpidirectorNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.Dpidirector)
                .HasConstraintName("DPI_Director_Direccion");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.Dpi).HasName("PRIMARY");

            entity.ToTable("director");

            entity.Property(e => e.Dpi).HasColumnName("DPI");

            entity.HasOne(d => d.DpiNavigation).WithOne(p => p.Director)
                .HasForeignKey<Director>(d => d.Dpi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DPI");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Dpiempleado).HasName("PRIMARY");

            entity.ToTable("empleado");

            entity.HasIndex(e => e.IdDepartamento, "idDepartamento_Departamento_Empleado_idx");

            entity.HasIndex(e => e.IdDireccion, "idDireccion_Direccion_Empleado_idx");

            entity.HasIndex(e => e.IdPuesto, "idPuesto_Puesto_Empleado_idx");

            entity.Property(e => e.Dpiempleado).HasColumnName("DPIEmpleado");
            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.IdDireccion).HasColumnName("idDireccion");
            entity.Property(e => e.IdPuesto).HasColumnName("idPuesto");

            entity.HasOne(d => d.DpiempleadoNavigation).WithOne(p => p.Empleado)
                .HasForeignKey<Empleado>(d => d.Dpiempleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DPI_Persona_Empleado");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("idDepartamento_Departamento_Empleado");

            entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdDireccion)
                .HasConstraintName("idDireccion_Direccion_Empleado");

            entity.HasOne(d => d.IdPuestoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdPuesto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("idPuesto_Puesto_Empleado");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Dpi).HasName("PRIMARY");

            entity.ToTable("persona");

            entity.Property(e => e.Dpi).HasColumnName("DPI");
            entity.Property(e => e.Contraseña).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaContratación).HasColumnType("date");
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PrimerApellido).HasMaxLength(100);
            entity.Property(e => e.Salario).HasPrecision(20);
            entity.Property(e => e.SegundoApellido).HasMaxLength(100);
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.IdPuesto).HasName("PRIMARY");

            entity.ToTable("puesto");

            entity.Property(e => e.IdPuesto).HasColumnName("idPuesto");
            entity.Property(e => e.DescripcionPuesto)
                .HasMaxLength(100)
                .HasColumnName("descripcionPuesto");
            entity.Property(e => e.NombrePuesto)
                .HasMaxLength(100)
                .HasColumnName("nombrePuesto");
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Idtarea).HasName("PRIMARY");

            entity.ToTable("tarea");

            entity.HasIndex(e => e.DpiempleadoAsignado, "DPI_Empleado_Tarea_idx");

            entity.Property(e => e.Idtarea).HasColumnName("idtarea");
            entity.Property(e => e.Comentarios)
                .HasMaxLength(100)
                .HasColumnName("comentarios");
            entity.Property(e => e.DpiempleadoAsignado).HasColumnName("DPIEmpleadoAsignado");
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaLimite)
                .HasColumnType("datetime")
                .HasColumnName("fechaLimite");
            entity.Property(e => e.NombreTarea)
                .HasMaxLength(100)
                .HasColumnName("nombreTarea");
            entity.Property(e => e.RequerimientosTarea)
                .HasMaxLength(100)
                .HasColumnName("requerimientosTarea");

            entity.HasOne(d => d.DpiempleadoAsignadoNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.DpiempleadoAsignado)
                .HasConstraintName("DPI_Empleado_Tarea");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
