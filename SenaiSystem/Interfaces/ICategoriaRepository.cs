using SenaiSystem.Models;

namespace SenaiSystem.Interfaces;
public interface ICategoriaRepository
{
    List<Categoria> ListarTodos();
    Categoria? BuscarPorId(int id);
    //List<Categoria> ListarPorUsuario(int id);
    Categoria? BuscarPorUsuario(int id, string nomeNota);
    void Cadastrar(Categoria categoria);
    void Atualizar(int id, Categoria categoria);
    void Deletar(int id);
}
