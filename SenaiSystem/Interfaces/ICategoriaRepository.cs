using SenaiSystem.DTOs;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Interfaces;
public interface ICategoriaRepository
{
    List<ListarCategoriaViewModel> ListarTodos();
    Categoria? BuscarPorId(int id);
    List<Categoria>? ListarCategoriaPorUsuario(int id);
    Categoria? BuscarPorUsuario(int id, string nomeNota);
    void Cadastrar(CadastroEditarCategoriaDto categoria);
    void Atualizar(int id, CadastroEditarCategoriaDto categoria);
    void Deletar(int id);
}
