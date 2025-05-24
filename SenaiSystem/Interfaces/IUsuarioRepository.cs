using SenaiSystem.DTOs;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Interface
{
    public interface IUsuarioRepository 
    {
        List<ListarUsuarioViewModel> ListarTodos();
        Usuario? BuscarPorEmailSenha(string email, string senha);
        void Cadastrar(CadastroEditarUsuarioDto usuario);
        void Atualizar(int id, CadastroEditarUsuarioDto usuario);    
        void Deletar(int id);
        Usuario? BuscarPorId(int id);
    }
}
