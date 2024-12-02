using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestionCitas.Models;

public partial class UsuarioRol
{
    public int Id { get; set; }

    [Display(Name = "Usuario")]


    public int? UsuarioId { get; set; }

    [Display(Name = "Rol")]


    public int? RolId { get; set; }

    [Display(Name = "Fecha Asignacion")]


    public DateTime? FechaAsignacion { get; set; }

    public virtual Role? Rol { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
