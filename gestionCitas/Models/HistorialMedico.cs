using System;
using System.Collections.Generic;

namespace gestionCitas.Models;

public partial class HistorialMedico
{
    public int Id { get; set; }

    public int? ConsultaId { get; set; }

    public string? Diagnostico { get; set; }

    public string? Tratamiento { get; set; }

    public string? Observaciones { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Consulta? Consulta { get; set; }
}
