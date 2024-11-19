using System;
using System.Collections.Generic;

namespace gestionCitas.Models;

public partial class Cita
{
    public int Id { get; set; }

    public int? PacienteId { get; set; }

    public int? MedicoId { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Motivo { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();

    public virtual ICollection<FichasMedica> FichasMedicas { get; set; } = new List<FichasMedica>();

    public virtual Medico? Medico { get; set; }

    public virtual Paciente? Paciente { get; set; }

    public virtual ICollection<Recordatorio> Recordatorios { get; set; } = new List<Recordatorio>();
}
