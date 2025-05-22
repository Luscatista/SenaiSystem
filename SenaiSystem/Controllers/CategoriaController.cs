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
    [Authorize]
    public IActionResult ListarTodos()
    {
        return Ok(_categoriasRepository.ListarTodos());
    }

    //[HttpGet("/buscar/Usuario")]
    //public IActionResult ListarCategoriaPorUsuario()
    //{
    //    return Ok(_categoriasRepository.ListarCategoriaPorUsuario());
    //}

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult BuscarPorId(int id)
    {
        return Ok(_categoriasRepository.BuscarPorId(id));
    }

    [HttpPost]
    [Authorize]
    public IActionResult Cadastrar(Categoria categoria)
    {
        _categoriasRepository.Cadastrar(categoria);
        return Created();
    }

    [HttpPut("{id}")]
    [Authorize]
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
    [Authorize]
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
