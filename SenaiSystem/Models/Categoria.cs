using System;
using System.Collections.Generic;

namespace SenaiSystem.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public int? IdNota { get; set; }

    public string Nome { get; set; } = null!;

    public virtual Nota? IdNotaNavigation { get; set; }

    public virtual ICollection<NotaCategorium> NotaCategoria { get; set; } = new List<NotaCategorium>();
}
