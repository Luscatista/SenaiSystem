using SenaiSystem.Context;
using SenaiSystem.Interfaces;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Repositories
{
    public class NotaCategoriaRepository : INotaCategoriaRepository
    {
        private readonly SenaiSystemContext _context;
        public NotaCategoriaRepository(SenaiSystemContext context)
        {
            _context = context;
        }

        public void Atualizar(int id, NotaCategoria notaCategoria)
        {
            var NotaCategoriaEncontrado = _context.NotaCategoria.FirstOrDefault(n => n.IdNotaCategoria == id);
            if (NotaCategoriaEncontrado == null)
            {
                throw new ArgumentNullException("Nota não encontrada.");
            }

            NotaCategoriaEncontrado.IdCategoria = notaCategoria.IdCategoria;
            NotaCategoriaEncontrado.IdNota = notaCategoria.IdNota;

            _context.SaveChanges();


        }

        public NotaCategoria? BuscarPorId(int id)
        {
            var notaCategoria = _context.NotaCategoria.Find(id);
            if (notaCategoria == null)
            {
                throw new ArgumentNullException("Nota não encontrada.");
            }
            return notaCategoria; ;
        }

        public void Cadastrar(NotaCategoria notaCategoria)
        {
            _context.NotaCategoria.Add(notaCategoria);

            _context.SaveChanges(); ;
        }

        public void Deletar(int id)
        {
            var notaCategoria = _context.NotaCategoria.Find(id);

            if (notaCategoria == null)
            {
                throw new ArgumentNullException("Nota não encontrada.");
            }

            _context.NotaCategoria.Remove(notaCategoria);

            _context.SaveChanges(); ;
        }

        public List<ListarNotaCategoriaViewModel> ListarTodos()
        {
            var notaCategorias = _context.NotaCategoria
                .Select(n => new ListarNotaCategoriaViewModel
                {
                    IdNotaCategoria = n.IdNotaCategoria,
                    IdNota = n.IdNota,
                })
                .ToList();
            return notaCategorias;
        }
    }
}
