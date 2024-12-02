using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestionCitas.Models;

public partial class Medico
{
    public int Id { get; set; }

    [Display(Name = "Usuario")]

    public int? UsuarioId { get; set; }

    public string? Nombre { get; set; }

    [Display(Name = "Especialidad")]

    public int? EspecialidadId { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    [Display(Name = "Horario Consulta Inicio")]


    public TimeOnly? HorarioConsultaInicio { get; set; }

    [Display(Name = "Horario Consulta Fin")]


    public TimeOnly? HorarioConsultaFin { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Especialidade? Especialidad { get; set; }

    [Display(Name = "Horarios Medicos")]


    public virtual ICollection<HorariosMedico> HorariosMedicos { get; set; } = new List<HorariosMedico>();

    public virtual Usuario? Usuario { get; set; }
}
