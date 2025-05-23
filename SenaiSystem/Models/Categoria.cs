using System;
using System.Collections.Generic;

namespace SenaiSystem.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string Nome { get; set; } = null!;

    public int IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<NotaCategoria> NotaCategoria { get; set; } = new List<NotaCategoria>();
}
