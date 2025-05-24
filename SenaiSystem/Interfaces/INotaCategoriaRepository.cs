using SenaiSystem.DTOs;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Interfaces
{
    public interface INotaCategoriaRepository
    {
        List<ListarNotaCategoriaViewModel> ListarTodos();
        void Cadastrar(CadastroEditarNotaCategoriaDto notaCategoria);
        void Atualizar(int id, CadastroEditarNotaCategoriaDto notaCategoria);
        void Deletar(int id);
        NotaCategoria BuscarPorId(int id);
    }
}
