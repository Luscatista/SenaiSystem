using SenaiSystem.Models;

namespace SenaiSystem.Interfaces;
public interface ICategoriaRepository
{
    List<Categoria> ListarTodos();
    Categoria BuscarPorId(int id);
    void Cadastrar(Categoria categoria);
    void Atualizar(int id, Categoria categoria);
    void Deletar(int id);
}
