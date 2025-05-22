using System;
using System.Collections.Generic;

namespace SenaiSystem.Models;

public partial class NotaCategoria
{
    public int IdNotaCategoria { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdNota { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Nota? IdNotaNavigation { get; set; }
}
