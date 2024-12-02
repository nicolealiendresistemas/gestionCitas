using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestionCitas.Models;

public partial class HorariosMedico
{
    public int Id { get; set; }

    [Display(Name = "Medico")]
    public int? MedicoId { get; set; }
    [Display(Name = "Dia Semana")]

    public string? DiaSemana { get; set; }

    [Display(Name = "Hora Inicio")]

    public TimeOnly? HoraInicio { get; set; }

    [Display(Name = "Hora Fin")]


    public TimeOnly? HoraFin { get; set; }

    public virtual Medico? Medico { get; set; }
}
