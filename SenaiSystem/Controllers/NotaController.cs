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

    [Authorize]
    [HttpGet]    
    public IActionResult ListarTodos()
    {
        return Ok(_notaRepository.ListarTodos());
    }

    [Authorize]
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

    [Authorize]
    [HttpPost]    
    public IActionResult Cadastrar(CadastroNotaDto nota)
    {
        _notaRepository.Cadastrar(nota);
        return Created();
    }

    [Authorize]
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

    [Authorize]
    [HttpDelete]  

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

    [Authorize]
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

    [Authorize]
    [HttpGet("notas/Usuario")]
    public IActionResult BuscarPorUsuario(int id)
    {
        var notas = _notaRepository.BuscarPorUsuario(id);
        if (notas == null)
        {
            return NotFound("Nota não encontrada.");
        }
        return Ok(notas);
    }

    [Authorize]
    [HttpGet("Buscar/Notas")]
    public IActionResult BuscarPorInformacao(string texto)
    {
        var notas = _notaRepository.BuscarPorInformacao(texto);
        if (notas.Count == 0)
        {
            return NotFound("Nota não encontrada.");
        }
        return Ok(notas);
    }
}
