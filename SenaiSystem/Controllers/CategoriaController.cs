using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiSystem.Interfaces;
using SenaiSystem.Models;
using SenaiSystem.Repositories;

namespace SenaiSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _categoriasRepository;
    public CategoriaController(ICategoriaRepository categoriaRepository)
    {
        _categoriasRepository = categoriaRepository;
    }

    [HttpGet]
    public IActionResult ListarTodos()
    {
        return Ok(_categoriasRepository.ListarTodos());
    }

    [HttpGet("/buscar/Usuario")]
    public IActionResult ListarCategoriaPorUsuario(int id)
    {
        return Ok(_categoriasRepository.ListarCategoriaPorUsuario(id));
    }

    [HttpGet("{id}")]
    
    public IActionResult BuscarPorId(int id)
    {
        return Ok(_categoriasRepository.BuscarPorId(id));
    }

    [HttpGet("buscar/{id}")]
   
    public IActionResult BuscarPorUsuario(int id)
    {
        return Ok(_categoriasRepository.BuscarPorId(id));
    }

    [HttpPost]
    
    public IActionResult Cadastrar(Categoria categoria)
    {
        _categoriasRepository.Cadastrar(categoria);
        return Created();
    }

    [HttpPut("{id}")]
    
    public IActionResult Editar(int id, Categoria categoria)
    {
        try
        {
            _categoriasRepository.Atualizar(id, categoria);
            return Ok(categoria);
        }
        catch (Exception)
        {
            return NotFound("Categoria não encontrada.");
        }
    }

    [HttpDelete("{id}")]
    
    public IActionResult Deletar(int id)
    {
        try
        {
            _categoriasRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception)
        {
            return NotFound("Categoria não encontrada.");
        }
    }
}
