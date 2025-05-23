﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiSystem.Interfaces;
using SenaiSystem.DTOs;
using SenaiSystem.Models;

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

        [Authorize]
        [HttpGet]        
        public IActionResult ListarTodos()
        {
            return Ok(_notaCategoriaRepository.ListarTodos());
        }

        [Authorize]
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
        [Authorize]
        [HttpPost]
        
        public IActionResult Cadastrar(CadastroEditarNotaCategoriaDto notaCategoria)
        {
            _notaCategoriaRepository.Cadastrar(notaCategoria);
            return Created("Nota cadastrada com sucesso", notaCategoria);
        }

        [Authorize]
        [HttpPut]
        
        public IActionResult Editar(int id, CadastroEditarNotaCategoriaDto notaCategoria)
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

        [Authorize]
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
