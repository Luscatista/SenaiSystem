using SenaiSystem.DTOs;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Interfaces;
public interface ICategoriaRepository
{
    List<ListarCategoriaViewModel> ListarTodos();
    Categoria? BuscarPorId(int id);
    List<Categoria>? BuscarPorUsuario(int id);
    Categoria? BuscarPorUsuarioeId(int id, string nomeNota);
    ListarCategoriaViewModel? Cadastrar(CadastroEditarCategoriaDto categoria);
    ListarCategoriaViewModel? Atualizar(int id, CadastroEditarCategoriaDto categoria);
    ListarCategoriaViewModel? Deletar(int id);
}
