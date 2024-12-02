using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestionCitas.Models;

public partial class Recordatorio
{
    public int Id { get; set; }

    [Display(Name = "Cita")]


    public int? CitaId { get; set; }

    [Display(Name = "Fecha Envio")]


    public DateTime? FechaEnvio { get; set; }

    [Display(Name = "Metodo Envio")]


    public string? MetodoEnvio { get; set; }

    public string? Mensaje { get; set; }

    public virtual Cita? Cita { get; set; }
}
