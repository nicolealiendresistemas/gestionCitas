using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestionCitas.Models;

public partial class Consulta
{
    public int Id { get; set; }

    [Display(Name = "Cita")]

    public int? CitaId { get; set; }

    [Display(Name = "Fecha Consulta")]

    public DateTime? FechaConsulta { get; set; }

    public string? Diagnostico { get; set; }

    public string? Observaciones { get; set; }

    public virtual Cita? Cita { get; set; }

    [Display(Name = "Historial Medico")]

    public virtual ICollection<HistorialMedico> HistorialMedicos { get; set; } = new List<HistorialMedico>();

    public virtual ICollection<Receta> Receta { get; set; } = new List<Receta>();
}
