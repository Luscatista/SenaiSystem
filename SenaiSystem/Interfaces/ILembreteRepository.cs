using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Interfaces;

public interface ILembreteRepository
{
    List<ListarLembreteViewModel> ListarTodos();
    Lembrete? BuscarPorId(int id);
    void Cadastrar(Lembrete lembrete);
    void Atualizar(int id, Lembrete lembrete);
    void Deletar(int id);
}
