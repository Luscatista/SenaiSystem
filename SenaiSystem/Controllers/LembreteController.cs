using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiSystem.Interfaces;
using SenaiSystem.Models;
using SenaiSystem.Repositories;

namespace SenaiSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LembreteController : ControllerBase
{
    private readonly ILembreteRepository _lembreteRepository;
    public LembreteController(ILembreteRepository lembreteRepository)
    {
        _lembreteRepository = lembreteRepository;
    }

    [HttpGet]
    public IActionResult ListarTodos()
    {
        return Ok(_lembreteRepository.ListarTodos());
    }

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(int id)
    {
        return Ok(_lembreteRepository.BuscarPorId(id));
    }

    [HttpPost]
    public IActionResult Cadastrar(Lembrete lembrete)
    {
        _lembreteRepository.Cadastrar(lembrete);
        return Created();
    }

    [HttpPut("{id}")]
    public IActionResult Editar(int id, Lembrete lembrete)
    {
        try
        {
            _lembreteRepository.Atualizar(id, lembrete);
            return Ok(lembrete);
        }
        catch (Exception)
        {
            return NotFound("Lembrete não encontrado.");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
        try
        {
            _lembreteRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception)
        {
            return NotFound("Lembrete não encontrado.");
        }
    }
}
