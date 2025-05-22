using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiSystem.DTOs;
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
    [Authorize]
    public IActionResult ListarTodos()
    {
        return Ok(_notaRepository.ListarTodos());
    }
    [HttpGet("{id}")]
    [Authorize]
    public IActionResult BuscarPorId(int id)
    {
        var notas = _notaRepository.BuscarPorId(id);
        if (notas == null)
        {
            return NotFound("Nota não encontrada.");
        }
        return Ok(notas);
    }

    //[HttpPost]
    //[Authorize]
    //public IActionResult Cadastrar(CadastroNotaDto nota)
    //{
    //    _notaRepository.Cadastrar(CadastroNotaDto);
    //    return Created();
    //}

    [HttpPut]
    [Authorize]

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
    [Authorize]

    public IActionResult Deletar(int id)
    {
        try
        {
            _notaRepository.Deletar(id);
            return NoContent();
        }
        catch (ArgumentNullException)
        {
            return NotFound("Nota não encontrado.");
        }
    }

    [HttpPut("notas/arquivadas/")]

    public IActionResult Arquivada(int id, Nota nota)
    {
        try
        {
            _notaRepository.Arquivada(id);
            return Ok(nota);
        }
        catch (Exception)
        {
            return NotFound("Nota não encontrado.");
        }
    }

}
