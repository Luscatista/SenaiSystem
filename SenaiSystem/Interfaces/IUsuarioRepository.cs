using SenaiSystem.Models;

namespace SenaiSystem.Interface
{
    public interface IUsuarioRepository 
    {
        List<Usuario> ListarTodos();
        void Cadastrar(Usuario usuario);
        Usuario? Atualizar(int id, Usuario usuario);    
        Usuario? Deletar(int id);
    }
}
