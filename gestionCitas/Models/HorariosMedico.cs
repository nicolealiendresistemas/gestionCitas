using System;
using System.Collections.Generic;

namespace gestionCitas.Models;

public partial class HorariosMedico
{
    public int Id { get; set; }

    public int? MedicoId { get; set; }

    public string? DiaSemana { get; set; }

    public TimeOnly? HoraInicio { get; set; }

    public TimeOnly? HoraFin { get; set; }

    public virtual Medico? Medico { get; set; }
}
