using SenaiSystem.Models;

namespace SenaiSystem.Interfaces;

public interface ILembreteRepository
{
    List<Lembrete> ListarTodos();
    Lembrete? BuscarPorId(int id);
    void Cadastrar(Lembrete lembrete);
    void Atualizar(int id, Lembrete lembrete);
    void Deletar(int id);
}
