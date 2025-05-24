using SenaiSystem.DTOs;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Interfaces;

public interface ILembreteRepository
{
    List<ListarLembreteViewModel> ListarTodos();
    Lembrete? BuscarPorId(int id);
    void Cadastrar(CadastroEditarLembreteDto lembrete);
    void Atualizar(int id, CadastroEditarLembreteDto lembrete);
    void Deletar(int id);
}
