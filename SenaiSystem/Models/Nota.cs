using System;
using System.Collections.Generic;

namespace SenaiSystem.Models;

public partial class Nota
{
    public int IdNota { get; set; }

    public int? IdUsuario { get; set; }

    public string Titulo { get; set; } = null!;

    public string Imagem { get; set; } = null!;

    public string? Conteudo { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime DataModificacao { get; set; }

    public bool? Arquivada { get; set; }

    public int? Prioridade { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Lembrete> Lembretes { get; set; } = new List<Lembrete>();

    public virtual ICollection<NotaCategoria> NotaCategoria { get; set; } = new List<NotaCategoria>();
}
