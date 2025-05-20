using SenaiSystem.context;
using SenaiSystem.Interfaces;
using SenaiSystem.Models;

namespace SenaiSystem.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly SenaiSystemContext _context;
    public CategoriaRepository(SenaiSystemContext context)
    {
        _context = context;
    }
    public List<Categoria> ListarTodos()
    {
        return _context.Categoria.ToList();
    }
    public Categoria? BuscarPorId(int id)
    {
        return _context.Categoria.FirstOrDefault(c => c.IdCategoria == id);
    }

    public void Cadastrar(Categoria categoria)
    {
        _context.Categoria.Add(categoria);
        _context.SaveChanges();
    }

    public void Atualizar(int id, Categoria categoria)
    {
        var categoriaAtual = _context.Categoria.FirstOrDefault(c => c.IdCategoria == id);
        if (categoriaAtual == null)
        {
            throw new Exception("Categoria não encontrada.");
        }

        categoriaAtual.Nome = categoria.Nome;

        _context.SaveChanges();
    }
    public void Deletar(int id)
    {
        var categoria = _context.Categoria.FirstOrDefault(c => c.IdCategoria == id);
        if (categoria == null)
        {
            throw new Exception("Categoria não encontrada.");
        }

        _context.Categoria.Remove(categoria);
        _context.SaveChanges();
    }
}
