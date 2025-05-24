using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiSystem.DTOs;
using SenaiSystem.Interface;
using SenaiSystem.Models;
using SenaiSystem.Services;
using SenaiSystem.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace SenaiSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            var usuario = _usuarioRepository.ListarTodos();

            return Ok(usuario);
        }

        [Authorize]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria um usuário.(EXEMPLO)",
            Description = "Este endpoint cria usuários.(EXEMPLO)"

        )]
        public IActionResult Cadastrar(Models.Usuario usuario)
        {
            _usuarioRepository.Cadastrar(usuario);
            return Created("Usuario cadastrado com sucesso", usuario);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Models.Usuario usuario)
        {
            try
            {
                _usuarioRepository.Atualizar(id, usuario);
                return Ok(usuario);
            }
            catch (Exception)
            {
                return NotFound("Nota não encontrado.");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound("Nota não encontrado.");
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {

            var usuario = _usuarioRepository.BuscarPorEmailSenha(loginDto.Email, loginDto.Senha);
            if (usuario == null)
            {
                return Unauthorized("Dados inválidos.");
            }
            var usuarioViewModel = new ListarUsuarioViewModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email
            };

            var tokenService = new TokenService();

            var token = tokenService.GenerateToken(usuario.Email);

            return Ok(new
            {
                token,
                usuarioViewModel
            });
        }
    }
}
