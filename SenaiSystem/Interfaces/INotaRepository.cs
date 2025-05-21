using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Interfaces;

public interface INotaRepository
{
    List<NotaViewModel> ListarTodos();
    Nota BuscarPorId(int id);
    void Cadastrar(Nota nota);
    void Atualizar(int id, Nota nota);
    void Deletar(int id);
}
