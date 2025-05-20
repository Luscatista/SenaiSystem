using System;
using System.Collections.Generic;

namespace SenaiSystem.Models;

public partial class AuditoriaGeral
{
    public int IdAuditoria { get; set; }

    public string? NomeTabela { get; set; }

    public string? TipoAcao { get; set; }

    public string? Usuario { get; set; }

    public DateTime? DataAcao { get; set; }

    public string? DadosAntigos { get; set; }

    public string? DadosNovos { get; set; }
}
