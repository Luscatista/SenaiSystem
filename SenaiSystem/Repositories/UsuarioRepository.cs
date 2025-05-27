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
        public ListarUsuarioViewModel? Atualizar(int id, CadastroEditarUsuarioDto usuario)
        {
            var usuarioEncontrado = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

            if (usuarioEncontrado == null)
                return null;

            var passwordService = new PasswordService();

            usuarioEncontrado.Nome = usuario.Nome;
            usuarioEncontrado.Email = usuario.Email;
            usuarioEncontrado.Senha = usuario.Senha;

            usuarioEncontrado.Senha = passwordService.HashPassword(usuarioEncontrado);

            var novoUsuarioViewModel = new ListarUsuarioViewModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
            };

            _context.SaveChanges();
            return novoUsuarioViewModel;
            }
        public ListarUsuarioViewModel Deletar(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
                return null;

            _context.Usuarios.Remove(usuario);

            var novoUsuarioViewModel = new ListarUsuarioViewModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
            };

            _context.SaveChanges();

            return novoUsuarioViewModel;
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

        public ListarUsuarioViewModel? Cadastrar(CadastroEditarUsuarioDto usuario)
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

            var novoUsuarioViewModel = new ListarUsuarioViewModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
            };
            _context.SaveChanges();
            return novoUsuarioViewModel;
        }

        public ListarUsuarioViewModel? BuscarPorId(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return null;

            var novoUsuarioViewModel = new ListarUsuarioViewModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
            };
            return novoUsuarioViewModel;
        }

        public Usuario? BuscarPorEmailSenha(string email, string senha)
        {
            var usuario = _context.Usuarios.FirstOrDefault(c => c.Email == email);

            if (usuario == null)
                return null;

            var passwordService = new PasswordService();

            var resultado = passwordService.VerificarSenha(usuario, senha);


            if(resultado == true)
            {
                return usuario;
            }

            return null;
        }

        public ListarUsuarioViewModel? TrocarSenhaDto(int id, string senhaAtual, string novaSenha)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null) return null;

            var passwordService = new PasswordService();

            var resultado = passwordService.VerificarSenha(usuario, senhaAtual);


            if (resultado == false)
            {
                return null;
            }

            usuario.Senha = novaSenha;

            usuario.Senha = passwordService.HashPassword(usuario);

            _context.SaveChanges();

            var novoUsuarioViewModel = new ListarUsuarioViewModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
            };
            return novoUsuarioViewModel;
        }
    }
}
