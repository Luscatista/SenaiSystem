using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiSystem.DTOs;
using SenaiSystem.Interfaces;
using SenaiSystem.Models;
using SenaiSystem.Repositories;
using Swashbuckle.AspNetCore.Annotations;

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

    [Authorize]
    [HttpGet]
    [SwaggerOperation(
            Summary = "Lista todas as categorias",
            Description = "Este endpoint busca todas as categorias."

    )]
    public IActionResult ListarTodos()
    {
        return Ok(_categoriasRepository.ListarTodos());
    }

    [Authorize]
    [HttpGet("/buscar/Usuario")]
    [SwaggerOperation(
            Summary = "Lista todas as categorias por usuário.",
            Description = "Este endpoint encontra todas as categorias de um usuário especifico."

    )]

    public IActionResult BuscarPorUsuario(int id)
    {
        return Ok(_categoriasRepository.BuscarPorUsuario(id));
    }

    [Authorize]
    [HttpGet("{id}")]
    [SwaggerOperation(
            Summary = "Busca a categoria por Id",
            Description = "Este endpoint encontra a categoria por id."

    )]
    public IActionResult BuscarPorId(int id)
    {
        return Ok(_categoriasRepository.BuscarPorId(id));
    }

    [Authorize]
    [HttpGet("buscar/{id}")]
    [SwaggerOperation(
            Summary = "Resgata uma categoria por Id e Nome",
            Description = "Este endpoint busca categoria por id e nome."

    )]
    public IActionResult BuscarPorUsuarioeId (int id, string nome)
    {
        return Ok(_categoriasRepository.BuscarPorUsuarioeId(id, nome));
    }

    [Authorize]
    [HttpPost]
    [SwaggerOperation(
            Summary = "Cadastrar a categoria",
            Description = "Este endpoint cria a categoria."

    )]
    public IActionResult Cadastrar(CadastroEditarCategoriaDto categoria)
    {
        _categoriasRepository.Cadastrar(categoria);
        return Created();
    }

    [Authorize]
    [HttpPut("{id}")]
    [SwaggerOperation(
            Summary = "Atualiza Categoria",
            Description = "Este endpoint atualiza a categoria."

    )]

    public IActionResult Editar(int id, CadastroEditarCategoriaDto categoria)
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

    [Authorize]
    [HttpDelete("{id}")]
    [SwaggerOperation(
            Summary = "Deleta Categorias",
            Description = "Este endpoint deleta categorias."

    )]
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
