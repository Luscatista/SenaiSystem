using System;
using System.Collections.Generic;

namespace SenaiSystem.Models;

public partial class NotaCategorium
{
    public int IdNotaCategoria { get; set; }

    public int? IdCategoria { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }
}
