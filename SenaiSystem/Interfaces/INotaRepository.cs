using SenaiSystem.DTOs;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Interfaces;

public interface INotaRepository
{
    List<NotaViewModel> ListarTodos();
    Nota? BuscarPorId(int id);
    CadastroNotaDto? Cadastrar(CadastroNotaDto nota);
    void Atualizar(int id, Nota nota);
    void Deletar(int id);
    Nota? Arquivada(int id);
}
