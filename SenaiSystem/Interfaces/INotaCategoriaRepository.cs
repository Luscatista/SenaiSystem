using SenaiSystem.Models;

namespace SenaiSystem.Interfaces
{
    public interface INotaCategoriaRepository
    {
        List<NotaCategoria> ListarTodos();
        void Cadastrar(NotaCategoria notaCategoria);
        void Atualizar(int id, NotaCategoria notaCategoria);
        void Deletar(int id);
        NotaCategoria BuscarPorId(int id);
    }
}
