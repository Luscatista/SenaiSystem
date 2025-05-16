using SenaiSystem.Models;

namespace SenaiSystem.Interface
{
    public interface IUsuarioRepository 
    {
        List<Usuario> ListarTodos();
        Usuario BuscarPorEmailSenha(string email, string senha);
        void Cadastrar(Usuario usuario);
        Usuario? Atualizar(int id, Usuario usuario);    
        Usuario? Deletar(int id);
    }
}
