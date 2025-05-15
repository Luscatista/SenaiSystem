using SenaiSystem.Context;
using SenaiSystem.Interface;
using SenaiSystem.Models;

namespace SenaiSystem.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SenaiSystemContext _context;
        public UsuarioRepository(SenaiSystemContext context)
        {
            _context = context;
        }
        public Usuario? Atualizar(int id, Usuario usuario)
        {
            var usuarioEncontrado = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

            if (usuarioEncontrado == null) return null;

            usuarioEncontrado.Nome = usuario.Nome;
            usuarioEncontrado.Email = usuario.Email;
            usuarioEncontrado.Senha = usuario.Senha;

            _context.SaveChanges();

            return usuarioEncontrado;
        }
        public Usuario? Deletar(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null) return null;

            _context.Usuarios.Remove(usuario);

            _context.SaveChanges();

            return usuario;
        }
        public List<Usuario> ListarTodos()
        {
            return _context.Usuarios.ToList();
        }

        public void Cadastrar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);

            _context.SaveChanges();
        }
    }
}
