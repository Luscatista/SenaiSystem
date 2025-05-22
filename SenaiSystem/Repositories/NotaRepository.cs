using Microsoft.EntityFrameworkCore;
using SenaiSystem.Context;
using SenaiSystem.DTOs;
using SenaiSystem.Interfaces;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Repositories;

public class NotaRepository : INotaRepository
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly SenaiSystemContext _context;
    public NotaRepository(SenaiSystemContext context, ICategoriaRepository categoriaRepository)
    {
        _context = context;
        _categoriaRepository = categoriaRepository;
    }
    public List<NotaViewModel> ListarTodos()
    {
        return _context.Nota
            .Include(n => n.NotaCategoria)
            .ThenInclude(nC => nC.IdCategoriaNavigation)
            .Select(n => new NotaViewModel{
                IdNota = n.IdNota,
                Titulo = n.Titulo,
                Imagem = n.Imagem,
                Conteudo = n.Conteudo,
                DataCriacao = n.DataCriacao,
                DataModificacao = n.DataModificacao,
                Arquivada = n.Arquivada,
                Prioridade = n.Prioridade,
                Categorias = n.NotaCategoria.Select(
                    nC => new CategoriaViewModel
                    {
                        IdCategoria = nC.IdCategoriaNavigation.IdCategoria,
                        Nome = nC.IdCategoriaNavigation.Nome
                    }).ToList()
            }).ToList();
    }
    public Nota? BuscarPorId(int id)
    {
        return _context.Nota.FirstOrDefault(n => n.IdNota == id);
    }
    //public CadastroNotaDto? Cadastrar(CadastroNotaDto nota)
    //{
    //    //1- Percorrer a lista de categorias
    //    //1.1 - Verificar se a categoria existe
    //    //1.2 - Se ela ja existe vou ter que pegar o Id dela
    //    //1.2 - Se não existe, vou ter que cadastrar ela


    //    /*List<int> IdCategorias = new List<int>();

    //    foreach (var item in nota.Categorias)
    //    {
    //        var tag = _categoriaRepository.BuscarUsuarioPorId(item.IdCategoria, item); 

    //        if (tag = null)
    //        {
    //            // TODO: Cadastrar a categoria
    //        }

    //        IdCategorias.Add(tag.IdCategoria);
            
    //    }


    //}
    public void Atualizar(int id, Nota nota)
    {
        var notaAtual = _context.Nota.FirstOrDefault(n => n.IdNota == id);
        if (notaAtual == null)
        {
            throw new Exception("Nota não encontrada.");
        }

        notaAtual.IdUsuario = nota.IdUsuario;
        notaAtual.Titulo = nota.Titulo;
        notaAtual.Imagem = nota.Imagem;
        notaAtual.Conteudo = nota.Conteudo;
        notaAtual.DataCriacao = nota.DataCriacao;
        notaAtual.DataModificacao = nota.DataModificacao;
        notaAtual.Arquivada = nota.Arquivada;
        notaAtual.Prioridade = nota.Prioridade;

        _context.SaveChanges();
    }
    public void Deletar(int id)
    {
        var nota = _context.Nota.FirstOrDefault(n => n.IdNota == id);
        if (nota == null)
        {
            throw new Exception("Nota não encontrada.");
        }

        _context.Nota.Remove(nota);
        _context.SaveChanges();
    }
    public Nota? Arquivada(int id)
    {
        var nota = _context.Nota.Find(id);
        if (nota == null) 
        {
            throw new ArgumentNullException("Nota não encontrada.");
        }
        nota.Arquivada = !nota.Arquivada;
        _context.SaveChanges();
        return nota;
    }

    public CadastroNotaDto? Cadastrar(CadastroNotaDto nota)
    {
        throw new NotImplementedException();
    }
}
