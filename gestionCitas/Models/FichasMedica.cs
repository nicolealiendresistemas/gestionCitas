using System;
using System.Collections.Generic;

namespace gestionCitas.Models;

public partial class FichasMedica
{
    public int Id { get; set; }

    public int? CitaId { get; set; }

    public string? Diagnostico { get; set; }

    public string? Tratamiento { get; set; }

    public string? Observaciones { get; set; }

    public virtual Cita? Cita { get; set; }
}
