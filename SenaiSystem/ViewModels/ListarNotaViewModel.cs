using SenaiSystem.Models;

namespace SenaiSystem.ViewModels;

public class ListarNotaViewModel
{
    public int IdNota { get; set; }

    public string Titulo { get; set; } = null!;

    public string Imagem { get; set; } = null!;

    public string? Conteudo { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime DataModificacao { get; set; }

    public bool? Arquivada { get; set; }

    public int? Prioridade { get; set; }

    public List<CategoriaViewModel> Categorias { get; set; }
}
