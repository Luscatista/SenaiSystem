using Microsoft.EntityFrameworkCore;
using SenaiSystem.Context;
using SenaiSystem.DTOs;
using SenaiSystem.Interfaces;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly SenaiSystemContext _context;
    public CategoriaRepository(SenaiSystemContext context)
    {
        _context = context;
    }
    public List<ListarCategoriaViewModel> ListarTodos()
    {
        var categorias = _context.Categoria
            .Select(c => new ListarCategoriaViewModel
            {
                IdCategoria = c.IdCategoria,
                Nome = c.Nome
            })
            .ToList();
        return categorias;
    }
    public Categoria? BuscarPorId(int id)
    {
        return _context.Categoria.FirstOrDefault(c => c.IdCategoria == id);
    }

    public ListarCategoriaViewModel? Cadastrar(CadastroEditarCategoriaDto categoria)
    {
        var novaCategoria = new Categoria
        {
            Nome = categoria.Nome,
            IdUsuario = categoria.IdUsuario
        };
        _context.Categoria.Add(novaCategoria);
        _context.SaveChanges();

        var categoriaViewModel = new ListarCategoriaViewModel
        {
            IdCategoria = novaCategoria.IdCategoria,
            Nome = novaCategoria.Nome
        };

        return categoriaViewModel;
    }

    public ListarCategoriaViewModel? Atualizar(int id, CadastroEditarCategoriaDto categoria)
    {
        var categoriaAtual = _context.Categoria.FirstOrDefault(c => c.IdCategoria == id);
        if (categoriaAtual == null)
            return null;

        categoriaAtual.Nome = categoria.Nome;
        categoriaAtual.IdUsuario = categoria.IdUsuario;

        _context.SaveChanges();

        var categoriaViewModel = new ListarCategoriaViewModel
        {
            IdCategoria = categoriaAtual.IdCategoria,
            Nome = categoriaAtual.Nome
        };

        return categoriaViewModel;
    }
    public ListarCategoriaViewModel? Deletar(int id)
    {
        var categoria = _context.Categoria.FirstOrDefault(c => c.IdCategoria == id);
        if (categoria == null)
        {
            throw new ArgumentNullException("Categoria não encontrada.");
        }

        _context.Categoria.Remove(categoria);
        _context.SaveChanges();

        var categoriaViewModel = new ListarCategoriaViewModel
        {
            IdCategoria = categoria.IdCategoria,
            Nome = categoria.Nome
        };

        return categoriaViewModel;
    }

    public Categoria? BuscarPorUsuarioeId(int id, string nomeNota)
    {
        var categorias = _context.Categoria.FirstOrDefault(c => c.IdUsuario == id && c.Nome == nomeNota);
        return categorias;
    }

    public List<Categoria>? BuscarPorUsuario(int id)
    {
        var categorias = _context.Categoria.ToList();
        List<Categoria> categoriasUsuario = new();

        foreach (var item in categorias)
        {
            if (item.IdUsuario == id)
            {
                categoriasUsuario.Add(item);
            }
        }

        return categoriasUsuario;
    }
}
