using System;
using System.Collections.Generic;

namespace SenaiSystem.Models;

public partial class Lembrete
{
    public int IdLembrete { get; set; }

    public int? IdNota { get; set; }

    public DateTime DataHora { get; set; }

    public virtual Nota? IdNotaNavigation { get; set; }
}
