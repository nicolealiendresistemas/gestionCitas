using System;
using System.Collections.Generic;

namespace gestionCitas.Models;

public partial class Medico
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public string? Nombre { get; set; }

    public int? EspecialidadId { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public TimeOnly? HorarioConsultaInicio { get; set; }

    public TimeOnly? HorarioConsultaFin { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Especialidade? Especialidad { get; set; }

    public virtual ICollection<HorariosMedico> HorariosMedicos { get; set; } = new List<HorariosMedico>();

    public virtual Usuario? Usuario { get; set; }
}
