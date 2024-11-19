using System;
using System.Collections.Generic;

namespace gestionCitas.Models;

public partial class UsuarioRol
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public int? RolId { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public virtual Role? Rol { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
