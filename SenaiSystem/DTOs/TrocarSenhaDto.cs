namespace SenaiSystem.DTOs
{
    public class TrocarSenhaDto
    {
        public string SenhaAtual { get; set; } = null!;
        public string NovaSenha { get; set; } = null!;
        public string ConfirmarNovaSenha { get; set; } = null!;
        public int IdUsuario { get; set; }
    }
}
