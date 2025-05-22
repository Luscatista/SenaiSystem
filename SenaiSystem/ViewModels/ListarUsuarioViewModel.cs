using SenaiSystem.Models;

namespace SenaiSystem.ViewModels
{
    public class ListarUsuarioViewModel
    {
        public int IdUsuario { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

    }
}
