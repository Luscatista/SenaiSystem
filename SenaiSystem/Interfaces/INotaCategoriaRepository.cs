using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Interfaces
{
    public interface INotaCategoriaRepository
    {
        List<ListarNotaCategoriaViewModel> ListarTodos();
        void Cadastrar(NotaCategoria notaCategoria);
        void Atualizar(int id, NotaCategoria notaCategoria);
        void Deletar(int id);
        NotaCategoria BuscarPorId(int id);
    }
}
