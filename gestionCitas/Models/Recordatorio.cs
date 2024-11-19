using System;
using System.Collections.Generic;

namespace gestionCitas.Models;

public partial class Recordatorio
{
    public int Id { get; set; }

    public int? CitaId { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public string? MetodoEnvio { get; set; }

    public string? Mensaje { get; set; }

    public virtual Cita? Cita { get; set; }
}
