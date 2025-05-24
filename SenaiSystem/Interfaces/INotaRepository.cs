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
    void Atualizar(int id, CadastroEditarNotaDto nota);
    void Deletar(int id);
    Nota? Arquivada(int id);
}
