using System;
using System.Collections.Generic;

namespace SenaiSystem.Models;

public partial class Categorium
{
    public int IdCategoria { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<NotaCategoria> NotaCategoria { get; set; } = new List<NotaCategoria>();
}
