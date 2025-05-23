namespace SenaiSystem.DTOs
{
    public class CadastroNotaDto
    {
        public int IdUsuario { get; set; }

        public string Titulo { get; set; } = null!;

        public string Imagem { get; set; } = null!;

        public string? Conteudo { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataModificacao { get; set; }

        public bool? Arquivada { get; set; }

        public int? Prioridade { get; set; }

        public List<string> Categorias { get; set; } 
    }
}
