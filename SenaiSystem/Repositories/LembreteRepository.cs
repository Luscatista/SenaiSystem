using SenaiSystem.Context;
using SenaiSystem.Interfaces;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Repositories;

public class LembreteRepository : ILembreteRepository
{
    private readonly SenaiSystemContext _context;
    public LembreteRepository(SenaiSystemContext context)
    {
        _context = context;
    }
    public List<ListarLembreteViewModel> ListarTodos()
    {
        var lembretes = _context.Lembretes
            .Select(l => new ListarLembreteViewModel
            {
                IdLembrete = l.IdLembrete,
                DataHora = l.DataHora,
            })
            .ToList();
        return lembretes;
    }
    public Lembrete? BuscarPorId(int id)
    {
        return _context.Lembretes.FirstOrDefault(l => l.IdLembrete == id);
    }
    public void Cadastrar(Lembrete lembrete)
    {
        _context.Lembretes.Add(lembrete);
        _context.SaveChanges();
    }
    public void Atualizar(int id, Lembrete lembrete)
    {
        var lembreteAtual = _context.Lembretes.FirstOrDefault(l => l.IdLembrete == id);
        if (lembreteAtual == null)
        {
            throw new ArgumentNullException("Lembrete não encontrado.");
        }

        lembreteAtual.IdNota = lembrete.IdNota;
        lembreteAtual.DataHora = lembrete.DataHora;

        _context.SaveChanges();
    }
    public void Deletar(int id)
    {
        var lembrete = _context.Lembretes.Find(id);
        if (lembrete == null)
        {
            throw new ArgumentNullException("Lembrete não encontrada.");
        }

        _context.Lembretes.Remove(lembrete);
        _context.SaveChanges();
    }
}
