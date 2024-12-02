using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestionCitas.Models;

public partial class HistorialMedico
{
    public int Id { get; set; }

    [Display(Name = "Consulta")]

    public int? ConsultaId { get; set; }

    public string? Diagnostico { get; set; }

    public string? Tratamiento { get; set; }

    public string? Observaciones { get; set; }

    [Display(Name = "Fecha Registro")]


    public DateTime? FechaRegistro { get; set; }

    public virtual Consulta? Consulta { get; set; }
}
