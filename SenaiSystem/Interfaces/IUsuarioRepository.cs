using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Interface
{
    public interface IUsuarioRepository 
    {
        List<ListarUsuarioViewModel> ListarTodos();
        Usuario? BuscarPorEmailSenha(string email, string senha);
        void Cadastrar(Usuario usuario);
        void Atualizar(int id, Usuario usuario);    
        void Deletar(int id);
        Usuario? BuscarPorId(int id);
    }
}
