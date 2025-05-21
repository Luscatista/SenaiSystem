using Microsoft.EntityFrameworkCore;
using SenaiSystem.context;
using SenaiSystem.Interfaces;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Repositories;

public class NotaRepository : INotaRepository
{
    private readonly SenaiSystemContext _context;
    public NotaRepository(SenaiSystemContext context)
    {
        _context = context;
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
    public void Cadastrar(Nota nota)
    {
        _context.Nota.Add(nota);
        _context.SaveChanges();
    }
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
}
