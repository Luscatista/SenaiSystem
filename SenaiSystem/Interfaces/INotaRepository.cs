using SenaiSystem.DTOs;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Interfaces;

public interface INotaRepository
{
    List<ListarNotaViewModel> ListarTodos();
    Nota? BuscarPorId(int id);
    List<Nota>? BuscarPorInformacao(string texto); 
    CadastroNotaDto? Cadastrar(CadastroNotaDto nota);
    List<ListarNotaViewModel> BuscarPorUsuario(int id);
    void Atualizar(int id, Nota nota);
    void Deletar(int id);
    Nota? Arquivada(int id);
}
