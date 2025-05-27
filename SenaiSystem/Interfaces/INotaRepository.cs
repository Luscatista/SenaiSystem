using SenaiSystem.DTOs;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Interfaces;

public interface INotaRepository
{
    List<ListarNotaViewModel> ListarTodos();
    Nota? BuscarPorId(int id);
    List<Nota>? BuscarPorInformacao(string texto); 
    CadastroEditarNotaDto? Cadastrar(CadastroEditarNotaDto nota);
    List<ListarNotaViewModel> BuscarPorUsuario(int id);
    Nota? Atualizar(int id, CadastroEditarNotaDto nota);
    Nota? Deletar(int id);
    Nota? Arquivada(int id);
}
