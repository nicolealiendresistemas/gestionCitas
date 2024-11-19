using System;
using System.Collections.Generic;

namespace gestionCitas.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<Medico> Medicos { get; set; } = new List<Medico>();

    public virtual ICollection<UsuarioRol> UsuarioRols { get; set; } = new List<UsuarioRol>();
}
