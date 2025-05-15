using Microsoft.EntityFrameworkCore;
using SenaiSystem.Context;
using SenaiSystem.Interfaces;
using SenaiSystem.Models;

namespace SenaiSystem.Repositories;

public class NotaRepository : INotaRepository
{
    private readonly SenaiSystemContext _context;
    public NotaRepository(SenaiSystemContext context)
    {
        _context = context;
    }
    public List<Nota> ListarTodos()
    {
        return _context.Nota.ToList();
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
