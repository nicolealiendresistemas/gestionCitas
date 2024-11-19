using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace gestionCitas.Models;

public partial class GestioncitasContext : DbContext
{
    public GestioncitasContext()
    {
    }

    public GestioncitasContext(DbContextOptions<GestioncitasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Consulta> Consultas { get; set; }

    public virtual DbSet<Especialidade> Especialidades { get; set; }

    public virtual DbSet<FichasMedica> FichasMedicas { get; set; }

    public virtual DbSet<HistorialMedico> HistorialMedicos { get; set; }

    public virtual DbSet<HorariosMedico> HorariosMedicos { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Receta> Recetas { get; set; }

    public virtual DbSet<Recordatorio> Recordatorios { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioRol> UsuarioRols { get; set; }
    public virtual DbSet<Ticket> Tickets { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-D9IF75B\\SQL2019;Database=gestioncitas;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__citas__3213E83F912C847D");

            entity.ToTable("citas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.MedicoId).HasColumnName("medico_id");
            entity.Property(e => e.Motivo)
                .HasColumnType("text")
                .HasColumnName("motivo");
            entity.Property(e => e.PacienteId).HasColumnName("paciente_id");

            entity.HasOne(d => d.Medico).WithMany(p => p.Cita)
                .HasForeignKey(d => d.MedicoId)
                .HasConstraintName("FK__citas__medico_id__5CD6CB2B");

            entity.HasOne(d => d.Paciente).WithMany(p => p.Cita)
                .HasForeignKey(d => d.PacienteId)
                .HasConstraintName("FK__citas__paciente___5BE2A6F2");
        });

        modelBuilder.Entity<Consulta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__consulta__3213E83F01E851CB");

            entity.ToTable("consultas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CitaId).HasColumnName("cita_id");
            entity.Property(e => e.Diagnostico)
                .HasColumnType("text")
                .HasColumnName("diagnostico");
            entity.Property(e => e.FechaConsulta)
                .HasColumnType("datetime")
                .HasColumnName("fecha_consulta");
            entity.Property(e => e.Observaciones)
                .HasColumnType("text")
                .HasColumnName("observaciones");

            entity.HasOne(d => d.Cita).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.CitaId)
                .HasConstraintName("FK__consultas__cita___68487DD7");
        });

        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__especial__3213E83F9A946669");

            entity.ToTable("especialidades");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<FichasMedica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__fichas_m__3213E83FEC33AAFA");

            entity.ToTable("fichas_medicas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CitaId).HasColumnName("cita_id");
            entity.Property(e => e.Diagnostico)
                .HasColumnType("text")
                .HasColumnName("diagnostico");
            entity.Property(e => e.Observaciones)
                .HasColumnType("text")
                .HasColumnName("observaciones");
            entity.Property(e => e.Tratamiento)
                .HasColumnType("text")
                .HasColumnName("tratamiento");

            entity.HasOne(d => d.Cita).WithMany(p => p.FichasMedicas)
                .HasForeignKey(d => d.CitaId)
                .HasConstraintName("FK__fichas_me__cita___656C112C");
        });

        modelBuilder.Entity<HistorialMedico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__historia__3213E83FFC81D3D1");

            entity.ToTable("historial_medico");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConsultaId).HasColumnName("consulta_id");
            entity.Property(e => e.Diagnostico)
                .HasColumnType("text")
                .HasColumnName("diagnostico");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Observaciones)
                .HasColumnType("text")
                .HasColumnName("observaciones");
            entity.Property(e => e.Tratamiento)
                .HasColumnType("text")
                .HasColumnName("tratamiento");

            entity.HasOne(d => d.Consulta).WithMany(p => p.HistorialMedicos)
                .HasForeignKey(d => d.ConsultaId)
                .HasConstraintName("FK__historial__consu__6E01572D");
        });

        modelBuilder.Entity<HorariosMedico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__horarios__3213E83FE510813F");

            entity.ToTable("horarios_medicos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DiaSemana)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("dia_semana");
            entity.Property(e => e.HoraFin).HasColumnName("hora_fin");
            entity.Property(e => e.HoraInicio).HasColumnName("hora_inicio");
            entity.Property(e => e.MedicoId).HasColumnName("medico_id");

            entity.HasOne(d => d.Medico).WithMany(p => p.HorariosMedicos)
                .HasForeignKey(d => d.MedicoId)
                .HasConstraintName("FK__horarios___medic__628FA481");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__medicos__3213E83F230CA830");

            entity.ToTable("medicos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EspecialidadId).HasColumnName("especialidad_id");
            entity.Property(e => e.HorarioConsultaFin).HasColumnName("horario_consulta_fin");
            entity.Property(e => e.HorarioConsultaInicio).HasColumnName("horario_consulta_inicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Especialidad).WithMany(p => p.Medicos)
                .HasForeignKey(d => d.EspecialidadId)
                .HasConstraintName("FK__medicos__especia__59063A47");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Medicos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__medicos__usuario__5812160E");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__paciente__3213E83F7624F51C");

            entity.ToTable("pacientes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Receta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__recetas__3213E83F8A3020E9");

            entity.ToTable("recetas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConsultaId).HasColumnName("consulta_id");
            entity.Property(e => e.Dosis)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("dosis");
            entity.Property(e => e.Instrucciones)
                .HasColumnType("text")
                .HasColumnName("instrucciones");
            entity.Property(e => e.Medicamento)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("medicamento");

            entity.HasOne(d => d.Consulta).WithMany(p => p.Receta)
                .HasForeignKey(d => d.ConsultaId)
                .HasConstraintName("FK__recetas__consult__6B24EA82");
        });

        modelBuilder.Entity<Recordatorio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__recordat__3213E83FE9E22727");

            entity.ToTable("recordatorios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CitaId).HasColumnName("cita_id");
            entity.Property(e => e.FechaEnvio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_envio");
            entity.Property(e => e.Mensaje)
                .HasColumnType("text")
                .HasColumnName("mensaje");
            entity.Property(e => e.MetodoEnvio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metodo_envio");

            entity.HasOne(d => d.Cita).WithMany(p => p.Recordatorios)
                .HasForeignKey(d => d.CitaId)
                .HasConstraintName("FK__recordato__cita___5FB337D6");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F2B868415");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuarios__3213E83F015CE0A2");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<UsuarioRol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario___3213E83F3A4A45A1");

            entity.ToTable("usuario_rol");

            entity.HasIndex(e => new { e.UsuarioId, e.RolId }, "UQ_usuario_rol").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_asignacion");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Rol).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__usuario_r__rol_i__5165187F");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__usuario_r__usuar__5070F446");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
