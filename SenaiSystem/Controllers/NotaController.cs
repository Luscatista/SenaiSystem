using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiSystem.DTOs;
using SenaiSystem.Interfaces;
using SenaiSystem.Models;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(
            Summary = "Lista todas as notas",
            Description = "Este endpoint busca todas as notas."

    )]
    public IActionResult ListarTodos()
    {
        return Ok(_notaRepository.ListarTodos());
    }

    [Authorize]
    [HttpGet("{id}")]
    [SwaggerOperation(
            Summary = "Busca a nota por id",
            Description = "Este endpoint busca a nota pelo id informado."

    )]
    public IActionResult BuscarPorId(int id)
    {
        var notas = _notaRepository.BuscarPorId(id);
        if (notas == null)
        {
            return NotFound("Nota não encontrada.");
        }
        return Ok(notas);
    }
    
    //[Authorize]
    [HttpPost]
    [SwaggerOperation(
            Summary = "Cria uma nota",
            Description = "Este endpoint cria uma nota."

    )]
    public IActionResult Cadastrar(CadastroEditarNotaDto nota)
    {

        if(nota != null)
        {
            //extra - VERIFICAR SE O ARQUIVO É UMA IMAGEM

            //1 criar variavel que sera a pasta de destino
            var pastaDestino = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            //2 Salvar arquivo (extra - criar uma nome personalizado para o arquivo)
            var nomeArquivo = nota.ArquivoNota.FileName;

            var caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                nota.ArquivoNota.CopyTo(stream);
            }

            //3 Guardar o local do arquivo no BD

            nota.Imagem = nomeArquivo;
        }


        _notaRepository.Cadastrar(nota);
        return Created();
    }

    [Authorize]
    [HttpPut]
    [SwaggerOperation(
            Summary = "Atualiza uma nota",
            Description = "Este endpoint atualiza a nota informada."

    )]
    public IActionResult Editar(int id, CadastroEditarNotaDto nota)
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
    [SwaggerOperation(
            Summary = "Deleta uma nota",
            Description = "Este endpoint deleta uma nota especifica."

    )]
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
    [SwaggerOperation(
            Summary = "Arquiva a nota informada",
            Description = "Este endpoint arquiva as notas."

    )]
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
    [SwaggerOperation(
            Summary = "Busca todas as notas de um usuário",
            Description = "Este endpoint busca todas as notas de um usuário por seu id."

    )]
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
    [SwaggerOperation(
            Summary = "Busca todas as notas segundo a pesquisa",
            Description = "Este endpoint busca todas as notas de acordo com um texto informado."

    )]
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
