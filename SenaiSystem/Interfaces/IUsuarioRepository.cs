using SenaiSystem.DTOs;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Interface
{
    public interface IUsuarioRepository 
    {
        List<ListarUsuarioViewModel> ListarTodos();
        Usuario? BuscarPorEmailSenha(string email, string senha);
        ListarUsuarioViewModel? Cadastrar(CadastroEditarUsuarioDto usuario);
        ListarUsuarioViewModel? Atualizar(int id, CadastroEditarUsuarioDto usuario);
        ListarUsuarioViewModel? Deletar(int id);
        ListarUsuarioViewModel? BuscarPorId(int id);
    }
}
