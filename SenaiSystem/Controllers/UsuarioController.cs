using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiSystem.Interface;
using SenaiSystem.Models;

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

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var usuario = _usuarioRepository.ListarTodos();

            return Ok(usuario);
        }
        [HttpPost]
        public IActionResult Cadastrar(Models.Usuario usuario)
        {
            _usuarioRepository.Cadastrar(usuario);
            return Created("Usuario cadastrado com sucesso", usuario);
        }
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
    }
}
