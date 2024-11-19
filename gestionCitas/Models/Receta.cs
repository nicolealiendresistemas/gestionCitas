using System;
using System.Collections.Generic;

namespace gestionCitas.Models;

public partial class Receta
{
    public int Id { get; set; }

    public int? ConsultaId { get; set; }

    public string? Medicamento { get; set; }

    public string? Dosis { get; set; }

    public string? Instrucciones { get; set; }

    public virtual Consulta? Consulta { get; set; }
}
