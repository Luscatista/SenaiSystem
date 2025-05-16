using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiSystem.Interfaces;

namespace SenaiSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaCategoriaController : ControllerBase
    {
        private readonly INotaCategoriaRepository _notaCategoriaRepository;
        public NotaCategoriaController(INotaCategoriaRepository notaCategoriaRepository)
        {
            _notaCategoriaRepository = notaCategoriaRepository;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_notaCategoriaRepository.ListarTodos());
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id) 
        {
            var notaCategoria = _notaCategoriaRepository.BuscarPorId(id);
            if (notaCategoria == null)
            {
                return NotFound("Nota não encontrada.");
            }
            return Ok(notaCategoria);
        }
        [HttpPost]
        public IActionResult Cadastrar(Models.NotaCategoria notaCategoria)
        {
            _notaCategoriaRepository.Cadastrar(notaCategoria);
            return Created("Nota cadastrada com sucesso", notaCategoria);
        }

        [HttpPut]
        public IActionResult Editar(int id, Models.NotaCategoria notaCategoria)
        {
            try
            {
                _notaCategoriaRepository.Atualizar(id, notaCategoria);
                return Ok(notaCategoria);
            }
            catch (Exception)
            {
                return NotFound("Nota não encontrado.");
            }
        }
        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            try
            {
                _notaCategoriaRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound("Nota não encontrado.");
            }
        }


    }
}
