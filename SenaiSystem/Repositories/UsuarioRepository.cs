using Microsoft.AspNetCore.Mvc;
using SenaiSystem.Context;
using SenaiSystem.DTOs;
using SenaiSystem.Interface;
using SenaiSystem.Models;
using SenaiSystem.Services;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SenaiSystemContext _context;
        public UsuarioRepository(SenaiSystemContext context)
        {
            _context = context;
        }
        public void Atualizar(int id, CadastroEditarUsuarioDto usuario)
        {
            var usuarioEncontrado = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

            if (usuarioEncontrado == null)
            {
                throw new ArgumentNullException("Nota não encontrada.");
            };
            var passwordService = new PasswordService();

            usuarioEncontrado.Nome = usuario.Nome;
            usuarioEncontrado.Email = usuario.Email;
            usuarioEncontrado.Senha = usuario.Senha;

            usuarioEncontrado.Senha = passwordService.HashPassword(usuarioEncontrado);

            _context.SaveChanges();
        }
        public void Deletar(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                throw new ArgumentNullException("Nota não encontrada.");
            }

            _context.Usuarios.Remove(usuario);

            _context.SaveChanges();
        }
        public List<ListarUsuarioViewModel> ListarTodos()
        {
            return _context.Usuarios.Select(
                l => new ListarUsuarioViewModel
                {
                    Nome = l.Nome,
                    IdUsuario = l.IdUsuario,
                    Email = l.Email
                })
                .OrderBy(l => l.Nome)
                .ToList();
        }

        public void Cadastrar(CadastroEditarUsuarioDto usuario)
        {
            var novoUsuario = new Usuario
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha
            };
            
            var passwordService = new PasswordService();
            novoUsuario.Senha = passwordService.HashPassword(novoUsuario);

            _context.Usuarios.Add(novoUsuario);
            _context.SaveChanges();
        }

        public Usuario? BuscarPorId(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                throw new ArgumentNullException("Nota não encontrada.");
            }
            return usuario;
        }

        public Usuario? BuscarPorEmailSenha(string email, string senha)
        {
            var usuario = _context.Usuarios.FirstOrDefault(c => c.Email == email);

            if (usuario == null)
            {
                throw new ArgumentNullException("Nota não encontrada.");
            }

            var passwordService = new PasswordService();

            var resultado = passwordService.VerificarSenha(usuario, senha);


            if(resultado == true)
            {
                return usuario;
            }

            return usuario;
        }
    }
}
