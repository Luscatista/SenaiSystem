using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiSystem.Interfaces;
using SenaiSystem.Models;

namespace SenaiSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotaController : ControllerBase
{
    private readonly INotaRepository _notaRepository;
    public NotaController(INotaRepository notaRepository)
    {
        _notaRepository = notaRepository;
    }

    [HttpGet]
    public IActionResult ListarTodos()
    {
        return Ok(_notaRepository.ListarTodos());
    }
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(int id)
    {
        var notas = _notaRepository.BuscarPorId(id);
        if (notas == null)
        {
            return NotFound("Nota não encontrada.");
        }
        return Ok(notas);
    }

    [HttpPost]
    public IActionResult Cadastrar(Nota nota)
    {
        _notaRepository.Cadastrar(nota);
        return Created();
    }

    [HttpPut]

    public IActionResult Editar(int id, Nota nota)
    {
        try
        {
            _notaRepository.Atualizar(id, nota);
            return Ok(nota);
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
            _notaRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception)
        {
            return NotFound("Nota não encontrado.");
        }
    }
}
