using SenaiSystem.Context;
using SenaiSystem.Interface;
using SenaiSystem.Models;
using SenaiSystem.Services;

namespace SenaiSystem.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SenaiSystemContext _context;
        public UsuarioRepository(SenaiSystemContext context)
        {
            _context = context;
        }
        public void Atualizar(int id, Usuario usuario)
        {
            var usuarioEncontrado = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

            if (usuarioEncontrado == null)
            {
                throw new Exception("Nota não encontrada.");
            };

            usuarioEncontrado.Nome = usuario.Nome;
            usuarioEncontrado.Email = usuario.Email;
            usuarioEncontrado.Senha = usuario.Senha;

            _context.SaveChanges();
        }
        public void Deletar(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                throw new Exception("Nota não encontrada.");
            }

            _context.Usuarios.Remove(usuario);

            _context.SaveChanges();
        }
        public List<Usuario> ListarTodos()
        {
            return _context.Usuarios.ToList();
        }

        public void Cadastrar(Usuario usuario)
        {
            var passwordService = new PasswordService();

            usuario.Senha = passwordService.HashPassword(usuario);

            _context.Usuarios.Add(usuario);

            _context.SaveChanges();
        }

        public Usuario? BuscarPorId(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return null;
            return usuario;
        }

    }
}
