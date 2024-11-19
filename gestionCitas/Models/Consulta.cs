using System;
using System.Collections.Generic;

namespace gestionCitas.Models;

public partial class Consulta
{
    public int Id { get; set; }

    public int? CitaId { get; set; }

    public DateTime? FechaConsulta { get; set; }

    public string? Diagnostico { get; set; }

    public string? Observaciones { get; set; }

    public virtual Cita? Cita { get; set; }

    public virtual ICollection<HistorialMedico> HistorialMedicos { get; set; } = new List<HistorialMedico>();

    public virtual ICollection<Receta> Receta { get; set; } = new List<Receta>();
}
